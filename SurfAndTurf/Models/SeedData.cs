using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SurfAndTurf.Data;
using System;
using System.Linq;
using static System.Net.WebRequestMethods;

namespace SurfAndTurf.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SurfAndTurfContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SurfAndTurfContext>>()))
            {

                // Look for any boards
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange
                        (
                        new Microsoft.AspNetCore.Identity.IdentityRole
                        {
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new Microsoft.AspNetCore.Identity.IdentityRole
                        {
                            Name = "Customer",
                            NormalizedName = "CUSTOMER"
                        }
                        );
                    context.SaveChanges();
                }

                
                if (context.SurfBoard.Any())
                {
                    return;   // DB has been seeded
                }

                context.SurfBoard.AddRange(
                    new SurfBoard
                    {
                        Name = "The Minilog",
                        Length = 6,
                        Width = 21,
                        Thickness = 2.75, 
                        Volume = 38.8,
                        Type = "Shortboard",
                        Price = 565,
                        Equipment = "",
                        ImageUrl = "https://kite-prod.b-cdn.net/13342-large_default/naish-hover-ascend-carbon-s26-2021-surf-foilboard.jpg"
                    },
                    new SurfBoard
                    {
                        Name = "The Wide Glider",
                        Length = 7.1,
                        Width = 21.75,
                        Thickness = 2.75,
                        Volume = 43.22,
                        Type = "Funboard",
                        Price = 685,
                        Equipment = "",
                        ImageUrl = "https://kite-prod.b-cdn.net/13969-large_default/armstrong-wing-surf-foilboard-2022.jpg"
                    },
                    new SurfBoard
                    {
                        Name = "The Golden Ratio",
                        Length = 6.3,
                        Width = 20.75,
                        Thickness = 2.9,
                        Volume = 43.22,
                        Type = "Funboard",
                        Price = 695,
                        Equipment = "Paddel",
                        ImageUrl = "https://kite-prod.b-cdn.net/10803-large_default/rrd-longsup-9-8-lte-y25-2020-sup.jpg"
                    },
                    new SurfBoard
                    {
                        Name = "Mahi Mahi",
                        Length = 5.4,
                        Width = 20.75,
                        Thickness = 2.3,
                        Volume = 29.39,
                        Type = "Fish",
                        Price = 545,
                        Equipment = "",
                        ImageUrl = "https://kite-prod.b-cdn.net/18422-thickbox_default/north-charge-2023-kite-surfboard.jpg"
                    },
                    new SurfBoard
                    {
                        Name = "The Emerald Glider",
                        Length = 9.2,
                        Width = 22.8,
                        Thickness = 2.8,
                        Volume = 65.4,
                        Type = "Longboard",
                        Price = 895,
                        Equipment = "",
                        ImageUrl = "https://kite-prod.b-cdn.net/14807-thickbox_default/duotone-whip-d-lab-2022-kite-surfboard.jpg"
                    },
                    new SurfBoard
                    {
                        Name = "The Bomb",
                        Length = 5.5,
                        Width = 21,
                        Thickness = 2.5,
                        Volume = 33.7,
                        Type = "Longboard",
                        Price = 645,
                        Equipment = "",
                        ImageUrl = "https://kite-prod.b-cdn.net/18574-thickbox_default/f-one-mitu-pro-flex-2023-kite-surfboard.jpg"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}