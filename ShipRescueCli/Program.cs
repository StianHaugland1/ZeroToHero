// See https://aka.ms/new-console-template for more information

var ships = """
                      AAA_Y: 0,-5,90
                      BCA_C: 10,20,95
                      SAC_F: 5,80,65
                      ARH_B: 100,45,60
                      XXX_S: 150,70,180
                      """;

var shipRadio = new ShipRadio(ships);
shipRadio.EstablishChannel();
Console.WriteLine("Hello, World!");