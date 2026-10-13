using System.Collections.Generic;

namespace Byd.GtfsManagement.Consts;

// islevi: GTFS route_type lookup tablosunun, kod indeksinin ve sabit tanimlarinin sozlesmesini tutar.
// sistemdeki gorevi: Seed akisi ve EF eslemesinin ayni ulasim turu kodlarini okumasini saglar.
public static class RouteTypeConsts
{
    public const string TableName = "RouteTypes";
    public const string CodeIndex = "IX_RouteTypes_Code";

    public const int TramCode = 0;
    public const int SubwayCode = 1;
    public const int RailCode = 2;
    public const int BusCode = 3;
    public const int FerryCode = 4;
    public const int CableTramCode = 5;
    public const int AerialLiftCode = 6;
    public const int FunicularCode = 7;
    public const int TrolleybusCode = 11;
    public const int MonorailCode = 12;

    public static readonly IReadOnlyList<(int Id, string Name, int Code)> Definitions =
    [
        (1, "Tram, streetcar or light rail", TramCode),
        (2, "Subway or metro", SubwayCode),
        (3, "Rail", RailCode),
        (4, "Bus", BusCode),
        (5, "Ferry", FerryCode),
        (6, "Cable tram", CableTramCode),
        (7, "Aerial lift", AerialLiftCode),
        (8, "Funicular", FunicularCode),
        (9, "Trolleybus", TrolleybusCode),
        (10, "Monorail", MonorailCode),
        // Genisletilmis GTFS route_type kodlari (Google Transit); bircok uretici temel kod yerine bunlari yazar.
        (11, "Railway Service", 100),
        (12, "High Speed Rail Service", 101),
        (13, "Long Distance Trains", 102),
        (14, "Inter Regional Rail Service", 103),
        (15, "Car Transport Rail Service", 104),
        (16, "Sleeper Rail Service", 105),
        (17, "Regional Rail Service", 106),
        (18, "Tourist Railway Service", 107),
        (19, "Rail Shuttle (Within Complex)", 108),
        (20, "Suburban Railway", 109),
        (21, "Replacement Rail Service", 110),
        (22, "Special Rail Service", 111),
        (23, "Lorry Transport Rail Service", 112),
        (24, "All Rail Services", 113),
        (25, "Cross-Country Rail Service", 114),
        (26, "Vehicle Transport Rail Service", 115),
        (27, "Rack and Pinion Railway", 116),
        (28, "Additional Rail Service", 117),
        (29, "Coach Service", 200),
        (30, "International Coach Service", 201),
        (31, "National Coach Service", 202),
        (32, "Shuttle Coach Service", 203),
        (33, "Regional Coach Service", 204),
        (34, "Special Coach Service", 205),
        (35, "Sightseeing Coach Service", 206),
        (36, "Tourist Coach Service", 207),
        (37, "Commuter Coach Service", 208),
        (38, "All Coach Services", 209),
        (39, "Urban Railway Service", 400),
        (40, "Metro Service", 401),
        (41, "Underground Service", 402),
        (42, "Urban Railway Service", 403),
        (43, "All Urban Railway Services", 404),
        (44, "Monorail", 405),
        (45, "Bus Service", 700),
        (46, "Regional Bus Service", 701),
        (47, "Express Bus Service", 702),
        (48, "Stopping Bus Service", 703),
        (49, "Local Bus Service", 704),
        (50, "Night Bus Service", 705),
        (51, "Post Bus Service", 706),
        (52, "Special Needs Bus", 707),
        (53, "Mobility Bus Service", 708),
        (54, "Mobility Bus for Registered Disabled", 709),
        (55, "Sightseeing Bus", 710),
        (56, "Shuttle Bus", 711),
        (57, "School Bus", 712),
        (58, "School and Public Service Bus", 713),
        (59, "Rail Replacement Bus Service", 714),
        (60, "Demand and Response Bus Service", 715),
        (61, "All Bus Services", 716),
        (62, "Trolleybus Service", 800),
        (63, "Tram Service", 900),
        (64, "City Tram Service", 901),
        (65, "Local Tram Service", 902),
        (66, "Regional Tram Service", 903),
        (67, "Sightseeing Tram Service", 904),
        (68, "Shuttle Tram Service", 905),
        (69, "All Tram Services", 906),
        (70, "Water Transport Service", 1000),
        (71, "Air Service", 1100),
        (72, "Ferry Service", 1200),
        (73, "Aerial Lift Service", 1300),
        (74, "Telecabin Service", 1301),
        (75, "Cable Car Service", 1302),
        (76, "Elevator Service", 1303),
        (77, "Chair Lift Service", 1304),
        (78, "Drag Lift Service", 1305),
        (79, "Small Telecabin Service", 1306),
        (80, "All Telecabin Services", 1307),
        (81, "Funicular Service", 1400),
        (82, "Taxi Service", 1500),
        (83, "Communal Taxi Service", 1501),
        (84, "Water Taxi Service", 1502),
        (85, "Rail Taxi Service", 1503),
        (86, "Bike Taxi Service", 1504),
        (87, "Licensed Taxi Service", 1505),
        (88, "Private Hire Service Vehicle", 1506),
        (89, "All Taxi Services", 1507),
        (90, "Miscellaneous Service", 1700),
        (91, "Horse-drawn Carriage", 1702)
    ];
}
