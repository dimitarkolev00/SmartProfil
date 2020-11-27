using AutoMapper;

namespace SmartProfil.AutoMapper
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}
