using System.Linq;
using FlashMapper.Internal.Implementations.GeneratingMappings;
using FlashMapper.Internal.Implementations.MatchingProperties;
using FlashMapper.Internal.Implementations.ParsingUserInput;
using FlashMapper.Internal.Implementations.Settings;
using FlashMapper.Internal.Utils;
using FlashMapper.Models;
using FlashMapper.Services;
using FlashMapper.Services.GeneratingMappings;
using FlashMapper.Services.MatchingProperties;
using FlashMapper.Services.ParsingUserInput;
using FlashMapper.Services.Settings;

namespace FlashMapper.Internal
{
    internal static class ModuleConfiguration
    {
        public static FlashMapperDependencyResolver GetDefaultResolver()
        {
            var result = new FlashMapperDependencyResolver();
            
            result.RegisterService<IPropertyValueExpressionResolver>(r => new PropertyValueExpressionResolver(new ISpecificPropertyValueExpressionResolver[]
            {
                r.GetService<IUserInputPropertyValueExpressionResolver>(),
                r.GetService<IAutomaticPropertyValueExpressionResolver>(),
            }.Union(r.GetServices<ICustomPropertyValueExpressionResolver>()), r.GetService<IFlashMapperSettings>()));

            result.RegisterService<IUserInputPropertyValueExpressionResolver>(r => new UserInputPropertyValueExpressionResolver());
            result.RegisterService<IAutomaticPropertyValueExpressionResolver>(r => new AutomaticPropertyValueExpressionResolver(r.GetService<IPropertyNameComparer>(), r.GetService<IFlashMapperSettings>()));
            result.RegisterService<IUserInputParser>(r => new UserInputParser(new ISpecificUserInputParser[]
            {
                r.GetService<IMemberInitUserInputParser>(),
                r.GetService<INewUserInputParser>()
            }.Union(r.GetServices<ICustomUserInputParser>())));
            result.RegisterService<IMemberInitUserInputParser>(r => new MemberInitUserInputParser());
            result.RegisterService<INewUserInputParser>(r => new NewUserInputParser());
            result.RegisterService<IMappingExpressionAutocompleteService>(r => new MappingExpressionAutocompleteService(r.GetService<IPropertyValueExpressionResolver>(), r.GetService<IUserInputParser>(), r.GetService<IMapExpressionPostProcessor>()));
            result.RegisterService<IMappingGenerator>(r => new MappingGenerator(r.GetService<IMappingExpressionAutocompleteService>(), r.GetService<IExpressionCompiler>(), r.GetService<IFlashMapperSettings>()));
            result.RegisterService<IPropertyNameComparer>(r => new PropertyNameComparer(r.GetService<IPropertyPrefixLocator>(), new ISpecificPropertyNameComparer[]
            {
                r.GetService<INamingUnspecifiedPropertyNameComparer>(),
                r.GetService<IIgnoreCasePropertyNameComparer>(),
                r.GetService<ITokenizedPropertyNameComparer>()
            }.Union(r.GetServices<ICustomPropertyNameComparer>()), r.GetService<IFlashMapperSettings>()));
            result.RegisterService<INamingUnspecifiedPropertyNameComparer>(r => new NamingUnspecifiedPropertyNameComparer(r.GetService<IFlashMapperSettings>()));
            result.RegisterService<IIgnoreCasePropertyNameComparer>(r => new IgnoreCasePropertyNameComparer(r.GetService<IFlashMapperSettings>()));
            result.RegisterService<ITokenizedPropertyNameComparer>(r => new TokenizedPropertyNameComparer(r.GetService<IPropertyNameTokenizerResolver>(), r.GetService<IPropertyNameAbbrevationHandler>(), r.GetService<IFlashMapperSettings>()));
            result.RegisterService<IPropertyNameTokenizerResolver>(r => new PropertyNameTokenizerResolver(new IPropertyNameTokenizerFactory[]
            {
                r.GetService<ICamelCasePropertyNameTokenizerFactory>(),
                r.GetService<IUnderscoreSeparatedPropertyNameTokenizerFactory>()
            }.Union(r.GetServices<ICustomPropertyNameTokenizerFactory>())));
            result.RegisterService<ICamelCasePropertyNameTokenizerFactory>(r => new CamelCasePropertyNameTokenizerFactory());
            result.RegisterService<IUnderscoreSeparatedPropertyNameTokenizerFactory>(r => new UnderscoreSeparatedPropertyNameTokenizerFactory());
            result.RegisterService<IPropertyNameAbbrevationHandler>(r => new PropertyNameAbbrevationHandler());
            result.RegisterService<IMapExpressionPostProcessor>(r => new MapExpressionPostProcessor(new ISpecificMapExpressionPostProcessor[]
            {
                r.GetService<IPropertyAssignPostProcessor>(),
                r.GetService<IIsConstructionCheckPostProcessor>()
            }.Union(r.GetServices<ICustomMapExpressionPostProcessor>())));
            result.RegisterService<IIsConstructionCheckPostProcessor>(r => new IsConstructionCheckPostProcessor());
            result.RegisterService<IPropertyAssignPostProcessor>(r => new PropertyAssignPostProcessor(new ISpecificPropertyAssignMapExpressionBuilderFactory[]
            {
                r.GetService<IIgnorePropertyAssignMapExpressionBuilderFactory>(),
                r.GetService<IConditionalIgnorePropertyAssignMapExpressionBuilderFactory>()
            }.Union(r.GetServices<ICustomPropertyAssignMapExpressionBuilderFactory>())));
            result.RegisterService<IIgnorePropertyAssignMapExpressionBuilderFactory>(r => new IgnorePropertyAssignMapExpressionBuilderFactory(r.GetService<IIgnoreNodeChecker>()));
            result.RegisterService<IConditionalIgnorePropertyAssignMapExpressionBuilderFactory>(r => new ConditionalIgnorePropertyAssignMapExpressionBuilderFactory(r.GetService<IIgnoreNodeChecker>()));
            result.RegisterService<IIgnoreNodeChecker>(r => new IgnoreNodeChecker(new ISpecificIgnoreNodeChecker[]
            {
                r.GetService<IImplicitIgnoreNodeChecker>(),
                r.GetService<IExplicitIgnoreNodeChecker>()
            }.Union(r.GetServices<ICustomIgnoreNodeChecker>())));
            result.RegisterService<IImplicitIgnoreNodeChecker>(r => new ImplicitIgnoreNodeChecker());
            result.RegisterService<IExplicitIgnoreNodeChecker>(r => new ExplicitIgnoreNodeChecker(r.GetService<IImplicitIgnoreNodeChecker>()));
            result.RegisterService<IFlashMapperSettings>(r => new DefaultFlashMapperSettings());
#pragma warning disable 612
            result.RegisterService<IFlashMapperSettingsBuilderFactory>(r => new FlashMapperSettingsBuilderFactory());
#pragma warning restore 612
            result.RegisterService<IPropertyPrefixLocator>(r => new PropertyPrefixLocator());
            result.RegisterService<IMappingsStorageFactory>(r => new MappingsStorageFactory());
            result.RegisterService<IFlashMapperSettingsExtender>(r => new FlashMapperSettingsExtender());
#pragma warning disable 612
            result.RegisterService<IFlashMapperCustomServiceBuilderFactory>(r => new FlashMapperCustomServiceBuilderFactory(r));
#pragma warning restore 612
            result.RegisterService<IExpressionCompiler>(r => new ExpressionCompiler());
            return result;
        }
    }
}