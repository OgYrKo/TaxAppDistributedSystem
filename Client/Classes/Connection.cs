using InterfacesLibrary;
using System;
using System.ServiceModel;

namespace Client.Classes
{
    public class Connection<TInterface>
    {
        // Указание, где ожидать входящие сообщения.
        Uri address;

        // Указание, как обмениваться сообщениями.
        BasicHttpBinding binding;

        // Создание Конечной Точки.
        EndpointAddress endpoint;

        // Создание фабрики каналов.
        ChannelFactory<TInterface> factory;

        // Использование factory для создания канала (прокси).
        public TInterface channel { get; private set; }

        public TInterface channelWithUser
        {
            get
            {
                ((ISetUser)channel).SetUser(CurrentUser.Instance().Login,CurrentUser.Instance().Password);
                return channel;
            }
        }

        public Connection(string dirName)
        {
            address = new Uri("http://localhost:8080/Region1/" + dirName);
            binding = new BasicHttpBinding();
            binding.MaxReceivedMessageSize = 1000000;
            endpoint = new EndpointAddress(address);
            factory = new ChannelFactory<TInterface>(binding, endpoint);
            channel = factory.CreateChannel();
        }

    }
}
