﻿using ECommerceAPI.Application.Abstractions.Services.Configurations;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Dtos.Configuration;
using ECommerceAPI.Application.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace ECommerceAPI.Infrastructure.Services.Configurations
{
    /// <summary>
    /// Reflaction was applied !
    /// </summary>
    public class ApplicationService : IApplicationService
    {
        public List<Menu> GetAuthorizeDefinitionEndpoints(Type type)
        {
            //Assembly assembly = Assembly.GetExecutingAssembly();        // Mevcut layer'daki assembly 'leri getir !!!
            var assembly = Assembly.GetAssembly(type);
            var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));

            List<Menu> menus = new();

            if (controllers != null)
            {
                foreach (var controller in controllers)
                {
                    // methods in the controller
                    var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));

                    if (actions != null)
                    {
                        foreach (var action in actions)
                        {
                            var attributes = action.GetCustomAttributes(true);

                            if (attributes != null)
                            {
                                Menu menu = null;
                                var authorizeDefinitionAttribute = attributes.FirstOrDefault(x => x.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;

                                if (!menus.Any(m => m.Name == authorizeDefinitionAttribute?.Menu))
                                {
                                    menu = new() { Name = authorizeDefinitionAttribute?.Menu };
                                    menus.Add(menu);
                                }
                                    
                                else
                                    menu = menus.FirstOrDefault(x => x.Name == authorizeDefinitionAttribute?.Menu);

                                Application.Dtos.Configuration.Action _action = new()
                                {
                                    ActionType = Enum.GetName(typeof(ActionType), authorizeDefinitionAttribute.ActionType),
                                    Definition = authorizeDefinitionAttribute.Definition
                                };

                                var httpAttribute = attributes.FirstOrDefault(x => x.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;

                                if (httpAttribute != null)
                                    _action.HttpType = httpAttribute.HttpMethods.First();
                                else
                                    _action.HttpType = HttpMethods.Get;

                                _action.Code = $"{_action.HttpType}.{_action.ActionType}.{_action.Definition.Replace(" ", "")}";

                                menu.Actions.Add(_action);

                            }
                        }
                    }
                }
            }

            return menus;
        }
    }
}
