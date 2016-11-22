#r "..\Shared2\bin\Autofac.dll"
#r "..\Shared2\bin\Autofac.Extras.CommonServiceLocator.dll"

#r "..\Shared2\bin\AutoMapper.dll"

#r "..\Shared2\bin\Functions.EntityModels.dll"
#r "..\Shared2\bin\Functions.IoC.dll"
#r "..\Shared2\bin\Functions.Mappers.dll"
#r "..\Shared2\bin\Functions.Models.dll"
#r "..\Shared2\bin\Functions.Services.dll"

#r "..\Shared2\bin\Microsoft.Practices.ServiceLocation.dll"

using System;

using Functions.EntityModels;
using Functions.IoC;
using Functions.Mappers;
using Functions.Models;
using Functions.Services;

using Microsoft.Practices.ServiceLocation;

static Functions.IoC.ServiceLocator locator = new Functions.IoC.ServiceLocator();
