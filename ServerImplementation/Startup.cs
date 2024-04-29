using BankServiceInterfaces;
using CoreWCF;
using CoreWCF.Configuration;
using InterfacesLibrary;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RouterInterfaces;
using System.IO;

namespace ServerImplementation
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServiceModelServices();
        }
        public void Configure(IApplicationBuilder app)
        {
            IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

            string OfficeName = configuration["OfficeName"];

            app.UseServiceModel(builder =>
            {
                builder.AddService<AddressFunc>();
                builder.AddServiceEndpoint<AddressFunc, IAddressFunc>(new BasicHttpBinding(), $"/{OfficeName}/IAddressFunc");

                builder.AddService<CounterpartyFunc>();
                builder.AddServiceEndpoint<CounterpartyFunc, ICounterpartyFunc>(new BasicHttpBinding(), $"/{OfficeName}/ICounterpartyFunc");

                builder.AddService<CounterpartyProperties>();
                builder.AddServiceEndpoint<CounterpartyProperties, ICounterpartyProperties>(new BasicHttpBinding(), $"/{OfficeName}/ICounterpartyProperties");

                builder.AddService<FullData>();
                builder.AddServiceEndpoint<FullData, IFullData>(new BasicHttpBinding(), $"/{OfficeName}/IFullData");

                builder.AddService<LandFunc>();
                builder.AddServiceEndpoint<LandFunc, ILandFunc>(new BasicHttpBinding(), $"/{OfficeName}/ILandFunc");

                builder.AddService<LandProperties>();
                builder.AddServiceEndpoint<LandProperties, ILandProperties>(new BasicHttpBinding(), $"/{OfficeName}/ILandProperties");

                builder.AddService<Nace>();
                builder.AddServiceEndpoint<Nace, INace>(new BasicHttpBinding(), $"/{OfficeName}/INace");

                builder.AddService<PropertyFunc>();
                builder.AddServiceEndpoint<PropertyFunc, IPropertyFunc>(new BasicHttpBinding(), $"/{OfficeName}/IPropertyFunc");

                builder.AddService<RealpropertyProperties>();
                builder.AddServiceEndpoint<RealpropertyProperties, IRealpropertyProperties>(new BasicHttpBinding(), $"/{OfficeName}/IRealpropertyProperties");

                builder.AddService<Specialpurpose>();
                builder.AddServiceEndpoint<Specialpurpose, ISpecialpurpose>(new BasicHttpBinding(), $"/{OfficeName}/ISpecialpurpose");

                builder.AddService<Settings>();
                builder.AddServiceEndpoint<Settings, ISettings>(new BasicHttpBinding(), $"/{OfficeName}/ISettings");

                builder.AddService<OrderImplementation>();
                builder.AddServiceEndpoint<OrderImplementation, IOrder>(new BasicHttpBinding(), $"/{OfficeName}/IOrder");

                builder.AddService<BankToServerImplementation>();
                builder.AddServiceEndpoint<BankToServerImplementation, IBankToServer>(new BasicHttpBinding(), $"/{OfficeName}/IBankToServer");

                builder.AddService<GraphicImplementation>();
                builder.AddServiceEndpoint<GraphicImplementation, IGraphics>(new BasicHttpBinding(), $"/{OfficeName}/IGraphics");

                builder.AddService<RegionOfficeImplementation>();
                builder.AddServiceEndpoint<RegionOfficeImplementation, IRegionOffice>(new BasicHttpBinding(), $"/{OfficeName}/IRegionOffice");
            });
        }
    }
}
