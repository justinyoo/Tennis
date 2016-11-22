#r "..\Shared\bin\Autofac.dll"
#r "..\Shared\bin\Autofac.Extras.CommonServiceLocator.dll"

#r "..\Shared\bin\AutoMapper.dll"

#r "..\Shared\bin\EntityFramework.dll"
#r "..\Shared\bin\EntityFramework.SqlServer.dll"

#r "..\Shared\bin\Microsoft.Practices.ServiceLocation.dll"

#r "..\Shared\bin\Newtonsoft.Json.dll"

#r "..\Shared\bin\Tennis.Common.dll"
#r "..\Shared\bin\Tennis.Common.IoC.dll"
#r "..\Shared\bin\Tennis.Mappers.dll"

#r "..\Shared\bin\Tournaments.EntityModels.dll"
#r "..\Shared\bin\Tournaments.Helpers.dll"
#r "..\Shared\bin\Tournaments.Mappers.dll"
#r "..\Shared\bin\Tournaments.Models.dll"
#r "..\Shared\bin\Tournaments.Services.dll"

using System;

using Microsoft.Practices.ServiceLocation;

using Tennis.Common.IoC;

using Tournaments.EntityModels;
using Tournaments.Mappers;
using Tournaments.Models;
using Tournaments.Services;

static Tennis.Common.IoC.IServiceLocator locator = new Tennis.Common.IoC.ServiceLocator();

