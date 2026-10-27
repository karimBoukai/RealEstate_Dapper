using RealEstate_Dapper_Api.Models.DapperContext;
using RealEstate_Dapper_Api.Repositories.AdsRepository;
using RealEstate_Dapper_Api.Repositories.AmenitiesRepository;
using RealEstate_Dapper_Api.Repositories.AppUserRepository;
using RealEstate_Dapper_Api.Repositories.BottomGridRepositories;
using RealEstate_Dapper_Api.Repositories.CategoryRepository;
using RealEstate_Dapper_Api.Repositories.ContactRepository;
using RealEstate_Dapper_Api.Repositories.EmployeeRepository;
using RealEstate_Dapper_Api.Repositories.EstateAgentRepository.DashboardRepository.ChartRepository;
using RealEstate_Dapper_Api.Repositories.EstateAgentRepository.DashboardRepository.LastProductsRepository;
using RealEstate_Dapper_Api.Repositories.EstateAgentRepository.DashboardRepository.StatisticRepository;
using RealEstate_Dapper_Api.Repositories.MessagesRepository;
using RealEstate_Dapper_Api.Repositories.PopularLocationsRepository;
using RealEstate_Dapper_Api.Repositories.ProductImageRepository;
using RealEstate_Dapper_Api.Repositories.ProductRepository;
using RealEstate_Dapper_Api.Repositories.PropertyAmenitiesRepository;
using RealEstate_Dapper_Api.Repositories.ServiceRepository;
using RealEstate_Dapper_Api.Repositories.StatisticsRepository;
using RealEstate_Dapper_Api.Repositories.SubFeatureRepositpry;
using RealEstate_Dapper_Api.Repositories.TestimonialRepository;
using RealEstate_Dapper_Api.Repositories.ToDoListRepository;
using RealEstate_Dapper_Api.Repositories.WhoWeAreRepsitory;

namespace RealEstate_Dapper_Api.Containers
{
    public static class Extensions
    {
        public static void ContainerDependencies(this IServiceCollection services)
        {
            services.AddTransient<Context>();


            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddTransient<IBottomGridRepository, BottomGridRepository>();
            services.AddTransient<IPopularLoacationsRepository, PopularLoacationsrepository>();
            services.AddTransient<ITestimonialRepository, TestimonialRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IStatisticsRepository, StatisticsRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IToDoListRepository, ToDoListRepository>();
            services.AddTransient<IWhoWeAreDetailRepository, WhoWeAreDetailRepository>();
            services.AddTransient<IStatisticRepository, StatisticRepository>();
            services.AddTransient<IChartRepository, ChartRepository>();
            services.AddTransient<ILast5ProductsRepository, Last5ProductsRepository>();
            services.AddTransient<IMessagesRepository, MessageRepository>();
            services.AddTransient<IProductImageRepository, ProductImageRepository>();
            services.AddTransient<IAppUserRepository, AppUserRepository>();
            services.AddTransient<IAmenitiesRepository, AmenitiesRepository>();
            services.AddTransient<IPropertyAmenitiesRepository, PropertyAmenitiesRepository>();
            services.AddTransient<ISubFeatureRepository, SubFeatureRepository>();
            services.AddTransient<IAdsRepository, AdsRepository>();

        }


    }
}
