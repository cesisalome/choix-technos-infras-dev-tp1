using Choix_des_technos_et_infras_de_développement___TP1.Application.Models;
using Choix_des_technos_et_infras_de_développement___TP1.Domain;
using Choix_des_technos_et_infras_de_développement___TP1.Persistence;
using Microsoft.EntityFrameworkCore;
using MQTTnet;
using MQTTnet.Client;

namespace Choix_des_technos_et_infras_de_développement___TP1.Application.User.Commands
{
    public class AddUserCommand : UseCaseBase
    {
        private IMqttClient _mqttClient { get; set; }

        public AddUserCommand(TP1Context dbContext) : base(dbContext) {
            var broker = "http://localhost";
            var port = 1883;
            var clientId = Guid.NewGuid().ToString();

            // Create a MQTT client factory
            var factory = new MqttFactory();

            // Create a MQTT client instance
            var mqttClient = factory.CreateMqttClient();

            // Create MQTT client options
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(broker, port) // MQTT broker address and port
                .WithClientId(clientId)
                .WithCleanSession()
                .Build();
            
            mqttClient.ConnectAsync(options);

            _mqttClient = mqttClient;
        }

        public async Task AddUserAsync(UserModel user, CancellationToken cancellationToken)
        {
            try
            {
                var profile = await _dbContext.Profiles
                    .Where(profile => profile.Name == user.ProfileName)
                    .FirstOrDefaultAsync(cancellationToken);

                if (profile == null)
                {
                    throw new Exception(string.Format("Profile not found : {0}", user.ProfileName));
                }

                var userToAdd = new UserEntity
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Profile = profile,
                };

                await _dbContext.Users.AddAsync(userToAdd, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                var mqttMessage = new MqttApplicationMessage
                {
                    PayloadSegment = new ArraySegment<byte>(BitConverter.GetBytes(userToAdd.Id)),
                    Topic = "Event/NewUser"
                };

                await _mqttClient.PublishAsync(mqttMessage, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
