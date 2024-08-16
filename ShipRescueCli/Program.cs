// See https://aka.ms/new-console-template for more information

using ShipRescue.Services;

var ships = """
                      AAA_Y: 0,-5,90
                      BCA_C: 10,20,95
                      SAC_F: 5,80,65
                      ARH_B: 100,45,60
                      XXX_S: 150,70,180
                      XXI_S: 1500,70,10
                      """;

var shipRadio = new ShipRadio(ships);
var result = shipRadio.EstablishChannel();
Console.WriteLine(result);