﻿global using OrderService.Domain.Entities;
global using Microsoft.EntityFrameworkCore;
global using MediatR;
global using FluentValidation;
global using OrderService.Application.Common.Interfaces.CQRC;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.FeatureManagement;
global using OrderService.Application.Behaviors;
global using OrderService.Application.Common.MassTransit;
global using System.Reflection;
global using OrderService.Application.Dtos;