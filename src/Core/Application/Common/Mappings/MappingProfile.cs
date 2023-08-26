using System.Reflection;
using AutoMapper;

namespace Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        AplicarMapeamentosDoAssembly(Assembly.GetExecutingAssembly());
    }

    private void AplicarMapeamentosDoAssembly(Assembly assembly)
    {
        var tipoIMapFrom = typeof(IMapFrom<>);

        var nomeMetodoMapeamento = nameof(IMapFrom<object>.Mapping);

        bool PossuiInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == tipoIMapFrom;

        var tipos = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(PossuiInterface)).ToList();

        var tiposArgumento = new Type[] { typeof(Profile) };

        foreach (var tipo in tipos)
        {
            var instancia = Activator.CreateInstance(tipo);

            var informacaoMetodo = tipo.GetMethod(nomeMetodoMapeamento);

            if (informacaoMetodo != null)
            {
                informacaoMetodo.Invoke(instancia, new object[] { this });
            }
            else
            {
                var interfaces = tipo.GetInterfaces().Where(PossuiInterface).ToList();

                if (interfaces.Count > 0)
                {
                    foreach (var interf in interfaces)
                    {
                        var informacaoMetodoInterf = interf.GetMethod(nomeMetodoMapeamento, tiposArgumento);

                        informacaoMetodoInterf.Invoke(instancia, new object[] { this });
                    }
                }
            }
        }
    }
}