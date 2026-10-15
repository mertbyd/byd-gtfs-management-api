using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Byd.GtfsManagement.Migrations
{
    /// <inheritdoc />
    public partial class OrganizationScopedFeedLifecycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_attributions_agencies_agency_id",
                schema: "gtfs_management",
                table: "Attributions");

            migrationBuilder.DropForeignKey(
                name: "fk_attributions_routes_route_id",
                schema: "gtfs_management",
                table: "Attributions");

            migrationBuilder.DropForeignKey(
                name: "fk_attributions_trips_trip_id",
                schema: "gtfs_management",
                table: "Attributions");

            migrationBuilder.DropForeignKey(
                name: "fk_calendar_dates_services_service_id",
                schema: "gtfs_management",
                table: "CalendarDates");

            migrationBuilder.DropForeignKey(
                name: "fk_calendars_services_service_id",
                schema: "gtfs_management",
                table: "Calendars");

            migrationBuilder.DropForeignKey(
                name: "fk_fare_attributes_agencies_agency_id",
                schema: "gtfs_management",
                table: "FareAttributes");

            migrationBuilder.DropForeignKey(
                name: "fk_fare_rules_fare_attributes_fare_attribute_id",
                schema: "gtfs_management",
                table: "FareRules");

            migrationBuilder.DropForeignKey(
                name: "fk_fare_rules_routes_route_id",
                schema: "gtfs_management",
                table: "FareRules");

            migrationBuilder.DropForeignKey(
                name: "fk_frequencies_trips_trip_id",
                schema: "gtfs_management",
                table: "Frequencies");

            migrationBuilder.DropForeignKey(
                name: "fk_pathways_stops_from_stop_id",
                schema: "gtfs_management",
                table: "Pathways");

            migrationBuilder.DropForeignKey(
                name: "fk_pathways_stops_to_stop_id",
                schema: "gtfs_management",
                table: "Pathways");

            migrationBuilder.DropForeignKey(
                name: "fk_routes_agencies_agency_id",
                schema: "gtfs_management",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "fk_routes_networks_network_id",
                schema: "gtfs_management",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "fk_stops_levels_level_id",
                schema: "gtfs_management",
                table: "Stops");

            migrationBuilder.DropForeignKey(
                name: "fk_stops_stops_parent_station_id",
                schema: "gtfs_management",
                table: "Stops");

            migrationBuilder.DropForeignKey(
                name: "fk_stop_times_location_groups_location_group_id",
                schema: "gtfs_management",
                table: "StopTimes");

            migrationBuilder.DropForeignKey(
                name: "fk_stop_times_stops_stop_id",
                schema: "gtfs_management",
                table: "StopTimes");

            migrationBuilder.DropForeignKey(
                name: "fk_stop_times_trips_trip_id",
                schema: "gtfs_management",
                table: "StopTimes");

            migrationBuilder.DropForeignKey(
                name: "fk_transfers_routes_from_route_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "fk_transfers_routes_to_route_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "fk_transfers_stops_from_stop_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "fk_transfers_stops_to_stop_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "fk_transfers_trips_from_trip_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "fk_transfers_trips_to_trip_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "fk_trips_routes_route_id",
                schema: "gtfs_management",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "fk_trips_services_service_id",
                schema: "gtfs_management",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_Tenant_Feed_GtfsTripId",
                schema: "gtfs_management",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "ix_trips_feed_id",
                schema: "gtfs_management",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "ix_transfers_from_route_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "ix_transfers_from_stop_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "ix_transfers_from_trip_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "ix_transfers_to_route_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "ix_transfers_to_stop_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "ix_transfers_to_trip_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "ix_stop_times_feed_id",
                schema: "gtfs_management",
                table: "StopTimes");

            migrationBuilder.DropIndex(
                name: "ix_stop_times_location_group_id",
                schema: "gtfs_management",
                table: "StopTimes");

            migrationBuilder.DropIndex(
                name: "IX_Stops_Tenant_Feed_GtfsStopId",
                schema: "gtfs_management",
                table: "Stops");

            migrationBuilder.DropIndex(
                name: "ix_stops_feed_id",
                schema: "gtfs_management",
                table: "Stops");

            migrationBuilder.DropIndex(
                name: "ix_stops_level_id",
                schema: "gtfs_management",
                table: "Stops");

            migrationBuilder.DropIndex(
                name: "IX_Services_Tenant_Feed_GtfsServiceId",
                schema: "gtfs_management",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "ix_services_feed_id",
                schema: "gtfs_management",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Routes_Tenant_Feed_GtfsRouteId",
                schema: "gtfs_management",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "ix_routes_feed_id",
                schema: "gtfs_management",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "ix_routes_network_id",
                schema: "gtfs_management",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "ix_pathways_from_stop_id",
                schema: "gtfs_management",
                table: "Pathways");

            migrationBuilder.DropIndex(
                name: "ix_pathways_to_stop_id",
                schema: "gtfs_management",
                table: "Pathways");

            migrationBuilder.DropIndex(
                name: "ix_frequencies_feed_id",
                schema: "gtfs_management",
                table: "Frequencies");

            migrationBuilder.DropIndex(
                name: "IX_Feeds_Tenant_Name",
                schema: "gtfs_management",
                table: "Feeds");

            migrationBuilder.DropIndex(
                name: "ix_fare_rules_fare_attribute_id",
                schema: "gtfs_management",
                table: "FareRules");

            migrationBuilder.DropIndex(
                name: "ix_fare_rules_route_id",
                schema: "gtfs_management",
                table: "FareRules");

            migrationBuilder.DropIndex(
                name: "ix_fare_attributes_agency_id",
                schema: "gtfs_management",
                table: "FareAttributes");

            migrationBuilder.DropIndex(
                name: "ix_calendars_feed_id",
                schema: "gtfs_management",
                table: "Calendars");

            migrationBuilder.DropIndex(
                name: "ix_calendar_dates_feed_id",
                schema: "gtfs_management",
                table: "CalendarDates");

            migrationBuilder.DropIndex(
                name: "ix_attributions_agency_id",
                schema: "gtfs_management",
                table: "Attributions");

            migrationBuilder.DropIndex(
                name: "ix_attributions_route_id",
                schema: "gtfs_management",
                table: "Attributions");

            migrationBuilder.DropIndex(
                name: "ix_attributions_trip_id",
                schema: "gtfs_management",
                table: "Attributions");

            migrationBuilder.DropIndex(
                name: "IX_Agencies_Tenant_Feed_GtfsAgencyId",
                schema: "gtfs_management",
                table: "Agencies");

            migrationBuilder.DropIndex(
                name: "IX_Agencies_Tenant_Feed_NoGtfsAgencyId",
                schema: "gtfs_management",
                table: "Agencies");

            migrationBuilder.DropIndex(
                name: "ix_agencies_feed_id",
                schema: "gtfs_management",
                table: "Agencies");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Translations");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Timeframes");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "StopTimes");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "StopAreas");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Shapes");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "RouteNetworks");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "RiderCategories");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Pathways");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Networks");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "LocationGroupStops");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "LocationGroups");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Frequencies");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "FeedInfos");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "FareTransferRules");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "FareRules");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "FareProducts");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "FareMedia");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "FareLegRules");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "FareLegJoinRules");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "FareAttributes");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "CalendarDates");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "BookingRules");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Attributions");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Agencies");

            migrationBuilder.AddColumn<string>(
                name: "failure_code",
                schema: "gtfs_management",
                table: "Feeds",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "last_operation_user_id",
                schema: "gtfs_management",
                table: "Feeds",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "organization_id",
                schema: "gtfs_management",
                table: "Feeds",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "source_type",
                schema: "gtfs_management",
                table: "Feeds",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "source_url",
                schema: "gtfs_management",
                table: "Feeds",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status",
                schema: "gtfs_management",
                table: "Feeds",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "stored_file_id",
                schema: "gtfs_management",
                table: "Feeds",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "stored_file_name",
                schema: "gtfs_management",
                table: "Feeds",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "validation_report",
                schema: "gtfs_management",
                table: "Feeds",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "ak_trips_feed_id_id",
                schema: "gtfs_management",
                table: "Trips",
                columns: new[] { "feed_id", "id" });

            migrationBuilder.AddUniqueConstraint(
                name: "ak_stops_feed_id_id",
                schema: "gtfs_management",
                table: "Stops",
                columns: new[] { "feed_id", "id" });

            migrationBuilder.AddUniqueConstraint(
                name: "ak_services_feed_id_id",
                schema: "gtfs_management",
                table: "Services",
                columns: new[] { "feed_id", "id" });

            migrationBuilder.AddUniqueConstraint(
                name: "ak_routes_feed_id_id",
                schema: "gtfs_management",
                table: "Routes",
                columns: new[] { "feed_id", "id" });

            migrationBuilder.AddUniqueConstraint(
                name: "ak_networks_feed_id_id",
                schema: "gtfs_management",
                table: "Networks",
                columns: new[] { "feed_id", "id" });

            migrationBuilder.AddUniqueConstraint(
                name: "ak_location_groups_feed_id_id",
                schema: "gtfs_management",
                table: "LocationGroups",
                columns: new[] { "feed_id", "id" });

            migrationBuilder.AddUniqueConstraint(
                name: "ak_levels_feed_id_id",
                schema: "gtfs_management",
                table: "Levels",
                columns: new[] { "feed_id", "id" });

            migrationBuilder.AddUniqueConstraint(
                name: "ak_fare_attributes_feed_id_id",
                schema: "gtfs_management",
                table: "FareAttributes",
                columns: new[] { "feed_id", "id" });

            migrationBuilder.AddUniqueConstraint(
                name: "ak_agencies_feed_id_id",
                schema: "gtfs_management",
                table: "Agencies",
                columns: new[] { "feed_id", "id" });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_Feed_GtfsTripId",
                schema: "gtfs_management",
                table: "Trips",
                columns: new[] { "feed_id", "gtfs_trip_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_trips_feed_id_route_id",
                schema: "gtfs_management",
                table: "Trips",
                columns: new[] { "feed_id", "route_id" });

            migrationBuilder.CreateIndex(
                name: "ix_trips_feed_id_service_id",
                schema: "gtfs_management",
                table: "Trips",
                columns: new[] { "feed_id", "service_id" });

            migrationBuilder.CreateIndex(
                name: "ix_transfers_feed_id_from_route_id",
                schema: "gtfs_management",
                table: "Transfers",
                columns: new[] { "feed_id", "from_route_id" });

            migrationBuilder.CreateIndex(
                name: "ix_transfers_feed_id_from_trip_id",
                schema: "gtfs_management",
                table: "Transfers",
                columns: new[] { "feed_id", "from_trip_id" });

            migrationBuilder.CreateIndex(
                name: "ix_transfers_feed_id_to_route_id",
                schema: "gtfs_management",
                table: "Transfers",
                columns: new[] { "feed_id", "to_route_id" });

            migrationBuilder.CreateIndex(
                name: "ix_transfers_feed_id_to_stop_id",
                schema: "gtfs_management",
                table: "Transfers",
                columns: new[] { "feed_id", "to_stop_id" });

            migrationBuilder.CreateIndex(
                name: "ix_transfers_feed_id_to_trip_id",
                schema: "gtfs_management",
                table: "Transfers",
                columns: new[] { "feed_id", "to_trip_id" });

            migrationBuilder.CreateIndex(
                name: "ix_stop_times_feed_id_location_group_id",
                schema: "gtfs_management",
                table: "StopTimes",
                columns: new[] { "feed_id", "location_group_id" });

            migrationBuilder.CreateIndex(
                name: "ix_stop_times_feed_id_stop_id",
                schema: "gtfs_management",
                table: "StopTimes",
                columns: new[] { "feed_id", "stop_id" });

            migrationBuilder.CreateIndex(
                name: "ix_stop_times_feed_id_trip_id",
                schema: "gtfs_management",
                table: "StopTimes",
                columns: new[] { "feed_id", "trip_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Stops_Feed_GtfsStopId",
                schema: "gtfs_management",
                table: "Stops",
                columns: new[] { "feed_id", "gtfs_stop_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_stops_feed_id_level_id",
                schema: "gtfs_management",
                table: "Stops",
                columns: new[] { "feed_id", "level_id" });

            migrationBuilder.CreateIndex(
                name: "ix_stops_feed_id_parent_station_id",
                schema: "gtfs_management",
                table: "Stops",
                columns: new[] { "feed_id", "parent_station_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Services_Feed_GtfsServiceId",
                schema: "gtfs_management",
                table: "Services",
                columns: new[] { "feed_id", "gtfs_service_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Routes_Feed_GtfsRouteId",
                schema: "gtfs_management",
                table: "Routes",
                columns: new[] { "feed_id", "gtfs_route_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_routes_feed_id_agency_id",
                schema: "gtfs_management",
                table: "Routes",
                columns: new[] { "feed_id", "agency_id" });

            migrationBuilder.CreateIndex(
                name: "ix_routes_feed_id_network_id",
                schema: "gtfs_management",
                table: "Routes",
                columns: new[] { "feed_id", "network_id" });

            migrationBuilder.CreateIndex(
                name: "ix_pathways_feed_id_from_stop_id",
                schema: "gtfs_management",
                table: "Pathways",
                columns: new[] { "feed_id", "from_stop_id" });

            migrationBuilder.CreateIndex(
                name: "ix_pathways_feed_id_to_stop_id",
                schema: "gtfs_management",
                table: "Pathways",
                columns: new[] { "feed_id", "to_stop_id" });

            migrationBuilder.CreateIndex(
                name: "ix_frequencies_feed_id_trip_id",
                schema: "gtfs_management",
                table: "Frequencies",
                columns: new[] { "feed_id", "trip_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_Tenant_Organization_Active",
                schema: "gtfs_management",
                table: "Feeds",
                columns: new[] { "tenant_id", "organization_id" },
                unique: true,
                filter: "is_active = true");

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_Tenant_Organization_Name",
                schema: "gtfs_management",
                table: "Feeds",
                columns: new[] { "tenant_id", "organization_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_fare_rules_feed_id_route_id",
                schema: "gtfs_management",
                table: "FareRules",
                columns: new[] { "feed_id", "route_id" });

            migrationBuilder.CreateIndex(
                name: "ix_fare_attributes_feed_id_agency_id",
                schema: "gtfs_management",
                table: "FareAttributes",
                columns: new[] { "feed_id", "agency_id" });

            migrationBuilder.CreateIndex(
                name: "ix_calendars_feed_id_service_id",
                schema: "gtfs_management",
                table: "Calendars",
                columns: new[] { "feed_id", "service_id" });

            migrationBuilder.CreateIndex(
                name: "ix_calendar_dates_feed_id_service_id",
                schema: "gtfs_management",
                table: "CalendarDates",
                columns: new[] { "feed_id", "service_id" });

            migrationBuilder.CreateIndex(
                name: "ix_attributions_feed_id_agency_id",
                schema: "gtfs_management",
                table: "Attributions",
                columns: new[] { "feed_id", "agency_id" });

            migrationBuilder.CreateIndex(
                name: "ix_attributions_feed_id_route_id",
                schema: "gtfs_management",
                table: "Attributions",
                columns: new[] { "feed_id", "route_id" });

            migrationBuilder.CreateIndex(
                name: "ix_attributions_feed_id_trip_id",
                schema: "gtfs_management",
                table: "Attributions",
                columns: new[] { "feed_id", "trip_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Agencies_Feed_GtfsAgencyId",
                schema: "gtfs_management",
                table: "Agencies",
                columns: new[] { "feed_id", "gtfs_agency_id" },
                unique: true,
                filter: "gtfs_agency_id IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Agencies_Feed_NoGtfsAgencyId",
                schema: "gtfs_management",
                table: "Agencies",
                column: "feed_id",
                unique: true,
                filter: "gtfs_agency_id IS NULL");

            migrationBuilder.AddForeignKey(
                name: "fk_attributions_agencies_feed_id_agency_id",
                schema: "gtfs_management",
                table: "Attributions",
                columns: new[] { "feed_id", "agency_id" },
                principalSchema: "gtfs_management",
                principalTable: "Agencies",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_attributions_routes_feed_id_route_id",
                schema: "gtfs_management",
                table: "Attributions",
                columns: new[] { "feed_id", "route_id" },
                principalSchema: "gtfs_management",
                principalTable: "Routes",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_attributions_trips_feed_id_trip_id",
                schema: "gtfs_management",
                table: "Attributions",
                columns: new[] { "feed_id", "trip_id" },
                principalSchema: "gtfs_management",
                principalTable: "Trips",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_calendar_dates_services_feed_id_service_id",
                schema: "gtfs_management",
                table: "CalendarDates",
                columns: new[] { "feed_id", "service_id" },
                principalSchema: "gtfs_management",
                principalTable: "Services",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_calendars_services_feed_id_service_id",
                schema: "gtfs_management",
                table: "Calendars",
                columns: new[] { "feed_id", "service_id" },
                principalSchema: "gtfs_management",
                principalTable: "Services",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_fare_attributes_agencies_feed_id_agency_id",
                schema: "gtfs_management",
                table: "FareAttributes",
                columns: new[] { "feed_id", "agency_id" },
                principalSchema: "gtfs_management",
                principalTable: "Agencies",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_fare_rules_fare_attributes_feed_id_fare_attribute_id",
                schema: "gtfs_management",
                table: "FareRules",
                columns: new[] { "feed_id", "fare_attribute_id" },
                principalSchema: "gtfs_management",
                principalTable: "FareAttributes",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_fare_rules_routes_feed_id_route_id",
                schema: "gtfs_management",
                table: "FareRules",
                columns: new[] { "feed_id", "route_id" },
                principalSchema: "gtfs_management",
                principalTable: "Routes",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_frequencies_trips_feed_id_trip_id",
                schema: "gtfs_management",
                table: "Frequencies",
                columns: new[] { "feed_id", "trip_id" },
                principalSchema: "gtfs_management",
                principalTable: "Trips",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_pathways_stops_feed_id_from_stop_id",
                schema: "gtfs_management",
                table: "Pathways",
                columns: new[] { "feed_id", "from_stop_id" },
                principalSchema: "gtfs_management",
                principalTable: "Stops",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_pathways_stops_feed_id_to_stop_id",
                schema: "gtfs_management",
                table: "Pathways",
                columns: new[] { "feed_id", "to_stop_id" },
                principalSchema: "gtfs_management",
                principalTable: "Stops",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_routes_agencies_feed_id_agency_id",
                schema: "gtfs_management",
                table: "Routes",
                columns: new[] { "feed_id", "agency_id" },
                principalSchema: "gtfs_management",
                principalTable: "Agencies",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_routes_networks_feed_id_network_id",
                schema: "gtfs_management",
                table: "Routes",
                columns: new[] { "feed_id", "network_id" },
                principalSchema: "gtfs_management",
                principalTable: "Networks",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_stops_levels_feed_id_level_id",
                schema: "gtfs_management",
                table: "Stops",
                columns: new[] { "feed_id", "level_id" },
                principalSchema: "gtfs_management",
                principalTable: "Levels",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_stops_stops_feed_id_parent_station_id",
                schema: "gtfs_management",
                table: "Stops",
                columns: new[] { "feed_id", "parent_station_id" },
                principalSchema: "gtfs_management",
                principalTable: "Stops",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_stop_times_location_groups_feed_id_location_group_id",
                schema: "gtfs_management",
                table: "StopTimes",
                columns: new[] { "feed_id", "location_group_id" },
                principalSchema: "gtfs_management",
                principalTable: "LocationGroups",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_stop_times_stops_feed_id_stop_id",
                schema: "gtfs_management",
                table: "StopTimes",
                columns: new[] { "feed_id", "stop_id" },
                principalSchema: "gtfs_management",
                principalTable: "Stops",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_stop_times_trips_feed_id_trip_id",
                schema: "gtfs_management",
                table: "StopTimes",
                columns: new[] { "feed_id", "trip_id" },
                principalSchema: "gtfs_management",
                principalTable: "Trips",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_transfers_routes_feed_id_from_route_id",
                schema: "gtfs_management",
                table: "Transfers",
                columns: new[] { "feed_id", "from_route_id" },
                principalSchema: "gtfs_management",
                principalTable: "Routes",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_transfers_routes_feed_id_to_route_id",
                schema: "gtfs_management",
                table: "Transfers",
                columns: new[] { "feed_id", "to_route_id" },
                principalSchema: "gtfs_management",
                principalTable: "Routes",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_transfers_stops_feed_id_from_stop_id",
                schema: "gtfs_management",
                table: "Transfers",
                columns: new[] { "feed_id", "from_stop_id" },
                principalSchema: "gtfs_management",
                principalTable: "Stops",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_transfers_stops_feed_id_to_stop_id",
                schema: "gtfs_management",
                table: "Transfers",
                columns: new[] { "feed_id", "to_stop_id" },
                principalSchema: "gtfs_management",
                principalTable: "Stops",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_transfers_trips_feed_id_from_trip_id",
                schema: "gtfs_management",
                table: "Transfers",
                columns: new[] { "feed_id", "from_trip_id" },
                principalSchema: "gtfs_management",
                principalTable: "Trips",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_transfers_trips_feed_id_to_trip_id",
                schema: "gtfs_management",
                table: "Transfers",
                columns: new[] { "feed_id", "to_trip_id" },
                principalSchema: "gtfs_management",
                principalTable: "Trips",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_trips_routes_feed_id_route_id",
                schema: "gtfs_management",
                table: "Trips",
                columns: new[] { "feed_id", "route_id" },
                principalSchema: "gtfs_management",
                principalTable: "Routes",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_trips_services_feed_id_service_id",
                schema: "gtfs_management",
                table: "Trips",
                columns: new[] { "feed_id", "service_id" },
                principalSchema: "gtfs_management",
                principalTable: "Services",
                principalColumns: new[] { "feed_id", "id" },
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_attributions_agencies_feed_id_agency_id",
                schema: "gtfs_management",
                table: "Attributions");

            migrationBuilder.DropForeignKey(
                name: "fk_attributions_routes_feed_id_route_id",
                schema: "gtfs_management",
                table: "Attributions");

            migrationBuilder.DropForeignKey(
                name: "fk_attributions_trips_feed_id_trip_id",
                schema: "gtfs_management",
                table: "Attributions");

            migrationBuilder.DropForeignKey(
                name: "fk_calendar_dates_services_feed_id_service_id",
                schema: "gtfs_management",
                table: "CalendarDates");

            migrationBuilder.DropForeignKey(
                name: "fk_calendars_services_feed_id_service_id",
                schema: "gtfs_management",
                table: "Calendars");

            migrationBuilder.DropForeignKey(
                name: "fk_fare_attributes_agencies_feed_id_agency_id",
                schema: "gtfs_management",
                table: "FareAttributes");

            migrationBuilder.DropForeignKey(
                name: "fk_fare_rules_fare_attributes_feed_id_fare_attribute_id",
                schema: "gtfs_management",
                table: "FareRules");

            migrationBuilder.DropForeignKey(
                name: "fk_fare_rules_routes_feed_id_route_id",
                schema: "gtfs_management",
                table: "FareRules");

            migrationBuilder.DropForeignKey(
                name: "fk_frequencies_trips_feed_id_trip_id",
                schema: "gtfs_management",
                table: "Frequencies");

            migrationBuilder.DropForeignKey(
                name: "fk_pathways_stops_feed_id_from_stop_id",
                schema: "gtfs_management",
                table: "Pathways");

            migrationBuilder.DropForeignKey(
                name: "fk_pathways_stops_feed_id_to_stop_id",
                schema: "gtfs_management",
                table: "Pathways");

            migrationBuilder.DropForeignKey(
                name: "fk_routes_agencies_feed_id_agency_id",
                schema: "gtfs_management",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "fk_routes_networks_feed_id_network_id",
                schema: "gtfs_management",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "fk_stops_levels_feed_id_level_id",
                schema: "gtfs_management",
                table: "Stops");

            migrationBuilder.DropForeignKey(
                name: "fk_stops_stops_feed_id_parent_station_id",
                schema: "gtfs_management",
                table: "Stops");

            migrationBuilder.DropForeignKey(
                name: "fk_stop_times_location_groups_feed_id_location_group_id",
                schema: "gtfs_management",
                table: "StopTimes");

            migrationBuilder.DropForeignKey(
                name: "fk_stop_times_stops_feed_id_stop_id",
                schema: "gtfs_management",
                table: "StopTimes");

            migrationBuilder.DropForeignKey(
                name: "fk_stop_times_trips_feed_id_trip_id",
                schema: "gtfs_management",
                table: "StopTimes");

            migrationBuilder.DropForeignKey(
                name: "fk_transfers_routes_feed_id_from_route_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "fk_transfers_routes_feed_id_to_route_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "fk_transfers_stops_feed_id_from_stop_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "fk_transfers_stops_feed_id_to_stop_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "fk_transfers_trips_feed_id_from_trip_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "fk_transfers_trips_feed_id_to_trip_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "fk_trips_routes_feed_id_route_id",
                schema: "gtfs_management",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "fk_trips_services_feed_id_service_id",
                schema: "gtfs_management",
                table: "Trips");

            migrationBuilder.DropUniqueConstraint(
                name: "ak_trips_feed_id_id",
                schema: "gtfs_management",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_Feed_GtfsTripId",
                schema: "gtfs_management",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "ix_trips_feed_id_route_id",
                schema: "gtfs_management",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "ix_trips_feed_id_service_id",
                schema: "gtfs_management",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "ix_transfers_feed_id_from_route_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "ix_transfers_feed_id_from_trip_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "ix_transfers_feed_id_to_route_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "ix_transfers_feed_id_to_stop_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "ix_transfers_feed_id_to_trip_id",
                schema: "gtfs_management",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "ix_stop_times_feed_id_location_group_id",
                schema: "gtfs_management",
                table: "StopTimes");

            migrationBuilder.DropIndex(
                name: "ix_stop_times_feed_id_stop_id",
                schema: "gtfs_management",
                table: "StopTimes");

            migrationBuilder.DropIndex(
                name: "ix_stop_times_feed_id_trip_id",
                schema: "gtfs_management",
                table: "StopTimes");

            migrationBuilder.DropUniqueConstraint(
                name: "ak_stops_feed_id_id",
                schema: "gtfs_management",
                table: "Stops");

            migrationBuilder.DropIndex(
                name: "IX_Stops_Feed_GtfsStopId",
                schema: "gtfs_management",
                table: "Stops");

            migrationBuilder.DropIndex(
                name: "ix_stops_feed_id_level_id",
                schema: "gtfs_management",
                table: "Stops");

            migrationBuilder.DropIndex(
                name: "ix_stops_feed_id_parent_station_id",
                schema: "gtfs_management",
                table: "Stops");

            migrationBuilder.DropUniqueConstraint(
                name: "ak_services_feed_id_id",
                schema: "gtfs_management",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_Feed_GtfsServiceId",
                schema: "gtfs_management",
                table: "Services");

            migrationBuilder.DropUniqueConstraint(
                name: "ak_routes_feed_id_id",
                schema: "gtfs_management",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_Feed_GtfsRouteId",
                schema: "gtfs_management",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "ix_routes_feed_id_agency_id",
                schema: "gtfs_management",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "ix_routes_feed_id_network_id",
                schema: "gtfs_management",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "ix_pathways_feed_id_from_stop_id",
                schema: "gtfs_management",
                table: "Pathways");

            migrationBuilder.DropIndex(
                name: "ix_pathways_feed_id_to_stop_id",
                schema: "gtfs_management",
                table: "Pathways");

            migrationBuilder.DropUniqueConstraint(
                name: "ak_networks_feed_id_id",
                schema: "gtfs_management",
                table: "Networks");

            migrationBuilder.DropUniqueConstraint(
                name: "ak_location_groups_feed_id_id",
                schema: "gtfs_management",
                table: "LocationGroups");

            migrationBuilder.DropUniqueConstraint(
                name: "ak_levels_feed_id_id",
                schema: "gtfs_management",
                table: "Levels");

            migrationBuilder.DropIndex(
                name: "ix_frequencies_feed_id_trip_id",
                schema: "gtfs_management",
                table: "Frequencies");

            migrationBuilder.DropIndex(
                name: "IX_Feeds_Tenant_Organization_Active",
                schema: "gtfs_management",
                table: "Feeds");

            migrationBuilder.DropIndex(
                name: "IX_Feeds_Tenant_Organization_Name",
                schema: "gtfs_management",
                table: "Feeds");

            migrationBuilder.DropIndex(
                name: "ix_fare_rules_feed_id_route_id",
                schema: "gtfs_management",
                table: "FareRules");

            migrationBuilder.DropUniqueConstraint(
                name: "ak_fare_attributes_feed_id_id",
                schema: "gtfs_management",
                table: "FareAttributes");

            migrationBuilder.DropIndex(
                name: "ix_fare_attributes_feed_id_agency_id",
                schema: "gtfs_management",
                table: "FareAttributes");

            migrationBuilder.DropIndex(
                name: "ix_calendars_feed_id_service_id",
                schema: "gtfs_management",
                table: "Calendars");

            migrationBuilder.DropIndex(
                name: "ix_calendar_dates_feed_id_service_id",
                schema: "gtfs_management",
                table: "CalendarDates");

            migrationBuilder.DropIndex(
                name: "ix_attributions_feed_id_agency_id",
                schema: "gtfs_management",
                table: "Attributions");

            migrationBuilder.DropIndex(
                name: "ix_attributions_feed_id_route_id",
                schema: "gtfs_management",
                table: "Attributions");

            migrationBuilder.DropIndex(
                name: "ix_attributions_feed_id_trip_id",
                schema: "gtfs_management",
                table: "Attributions");

            migrationBuilder.DropUniqueConstraint(
                name: "ak_agencies_feed_id_id",
                schema: "gtfs_management",
                table: "Agencies");

            migrationBuilder.DropIndex(
                name: "IX_Agencies_Feed_GtfsAgencyId",
                schema: "gtfs_management",
                table: "Agencies");

            migrationBuilder.DropIndex(
                name: "IX_Agencies_Feed_NoGtfsAgencyId",
                schema: "gtfs_management",
                table: "Agencies");

            migrationBuilder.DropColumn(
                name: "failure_code",
                schema: "gtfs_management",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "last_operation_user_id",
                schema: "gtfs_management",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "organization_id",
                schema: "gtfs_management",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "source_type",
                schema: "gtfs_management",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "source_url",
                schema: "gtfs_management",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "status",
                schema: "gtfs_management",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "stored_file_id",
                schema: "gtfs_management",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "stored_file_name",
                schema: "gtfs_management",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "validation_report",
                schema: "gtfs_management",
                table: "Feeds");

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Trips",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Translations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Transfers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Timeframes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "StopTimes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Stops",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "StopAreas",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Shapes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Services",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Routes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "RouteNetworks",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "RiderCategories",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Pathways",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Networks",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "LocationGroupStops",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "LocationGroups",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Levels",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Frequencies",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "FeedInfos",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "FareTransferRules",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "FareRules",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "FareProducts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "FareMedia",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "FareLegRules",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "FareLegJoinRules",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "FareAttributes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Calendars",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "CalendarDates",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "BookingRules",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Attributions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Areas",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "gtfs_management",
                table: "Agencies",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_Tenant_Feed_GtfsTripId",
                schema: "gtfs_management",
                table: "Trips",
                columns: new[] { "tenant_id", "feed_id", "gtfs_trip_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_trips_feed_id",
                schema: "gtfs_management",
                table: "Trips",
                column: "feed_id");

            migrationBuilder.CreateIndex(
                name: "ix_transfers_from_route_id",
                schema: "gtfs_management",
                table: "Transfers",
                column: "from_route_id");

            migrationBuilder.CreateIndex(
                name: "ix_transfers_from_stop_id",
                schema: "gtfs_management",
                table: "Transfers",
                column: "from_stop_id");

            migrationBuilder.CreateIndex(
                name: "ix_transfers_from_trip_id",
                schema: "gtfs_management",
                table: "Transfers",
                column: "from_trip_id");

            migrationBuilder.CreateIndex(
                name: "ix_transfers_to_route_id",
                schema: "gtfs_management",
                table: "Transfers",
                column: "to_route_id");

            migrationBuilder.CreateIndex(
                name: "ix_transfers_to_stop_id",
                schema: "gtfs_management",
                table: "Transfers",
                column: "to_stop_id");

            migrationBuilder.CreateIndex(
                name: "ix_transfers_to_trip_id",
                schema: "gtfs_management",
                table: "Transfers",
                column: "to_trip_id");

            migrationBuilder.CreateIndex(
                name: "ix_stop_times_feed_id",
                schema: "gtfs_management",
                table: "StopTimes",
                column: "feed_id");

            migrationBuilder.CreateIndex(
                name: "ix_stop_times_location_group_id",
                schema: "gtfs_management",
                table: "StopTimes",
                column: "location_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_Tenant_Feed_GtfsStopId",
                schema: "gtfs_management",
                table: "Stops",
                columns: new[] { "tenant_id", "feed_id", "gtfs_stop_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_stops_feed_id",
                schema: "gtfs_management",
                table: "Stops",
                column: "feed_id");

            migrationBuilder.CreateIndex(
                name: "ix_stops_level_id",
                schema: "gtfs_management",
                table: "Stops",
                column: "level_id");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Tenant_Feed_GtfsServiceId",
                schema: "gtfs_management",
                table: "Services",
                columns: new[] { "tenant_id", "feed_id", "gtfs_service_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_services_feed_id",
                schema: "gtfs_management",
                table: "Services",
                column: "feed_id");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_Tenant_Feed_GtfsRouteId",
                schema: "gtfs_management",
                table: "Routes",
                columns: new[] { "tenant_id", "feed_id", "gtfs_route_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_routes_feed_id",
                schema: "gtfs_management",
                table: "Routes",
                column: "feed_id");

            migrationBuilder.CreateIndex(
                name: "ix_routes_network_id",
                schema: "gtfs_management",
                table: "Routes",
                column: "network_id");

            migrationBuilder.CreateIndex(
                name: "ix_pathways_from_stop_id",
                schema: "gtfs_management",
                table: "Pathways",
                column: "from_stop_id");

            migrationBuilder.CreateIndex(
                name: "ix_pathways_to_stop_id",
                schema: "gtfs_management",
                table: "Pathways",
                column: "to_stop_id");

            migrationBuilder.CreateIndex(
                name: "ix_frequencies_feed_id",
                schema: "gtfs_management",
                table: "Frequencies",
                column: "feed_id");

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_Tenant_Name",
                schema: "gtfs_management",
                table: "Feeds",
                columns: new[] { "tenant_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_fare_rules_fare_attribute_id",
                schema: "gtfs_management",
                table: "FareRules",
                column: "fare_attribute_id");

            migrationBuilder.CreateIndex(
                name: "ix_fare_rules_route_id",
                schema: "gtfs_management",
                table: "FareRules",
                column: "route_id");

            migrationBuilder.CreateIndex(
                name: "ix_fare_attributes_agency_id",
                schema: "gtfs_management",
                table: "FareAttributes",
                column: "agency_id");

            migrationBuilder.CreateIndex(
                name: "ix_calendars_feed_id",
                schema: "gtfs_management",
                table: "Calendars",
                column: "feed_id");

            migrationBuilder.CreateIndex(
                name: "ix_calendar_dates_feed_id",
                schema: "gtfs_management",
                table: "CalendarDates",
                column: "feed_id");

            migrationBuilder.CreateIndex(
                name: "ix_attributions_agency_id",
                schema: "gtfs_management",
                table: "Attributions",
                column: "agency_id");

            migrationBuilder.CreateIndex(
                name: "ix_attributions_route_id",
                schema: "gtfs_management",
                table: "Attributions",
                column: "route_id");

            migrationBuilder.CreateIndex(
                name: "ix_attributions_trip_id",
                schema: "gtfs_management",
                table: "Attributions",
                column: "trip_id");

            migrationBuilder.CreateIndex(
                name: "IX_Agencies_Tenant_Feed_GtfsAgencyId",
                schema: "gtfs_management",
                table: "Agencies",
                columns: new[] { "tenant_id", "feed_id", "gtfs_agency_id" },
                unique: true,
                filter: "gtfs_agency_id IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Agencies_Tenant_Feed_NoGtfsAgencyId",
                schema: "gtfs_management",
                table: "Agencies",
                columns: new[] { "tenant_id", "feed_id" },
                unique: true,
                filter: "gtfs_agency_id IS NULL");

            migrationBuilder.CreateIndex(
                name: "ix_agencies_feed_id",
                schema: "gtfs_management",
                table: "Agencies",
                column: "feed_id");

            migrationBuilder.AddForeignKey(
                name: "fk_attributions_agencies_agency_id",
                schema: "gtfs_management",
                table: "Attributions",
                column: "agency_id",
                principalSchema: "gtfs_management",
                principalTable: "Agencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_attributions_routes_route_id",
                schema: "gtfs_management",
                table: "Attributions",
                column: "route_id",
                principalSchema: "gtfs_management",
                principalTable: "Routes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_attributions_trips_trip_id",
                schema: "gtfs_management",
                table: "Attributions",
                column: "trip_id",
                principalSchema: "gtfs_management",
                principalTable: "Trips",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_calendar_dates_services_service_id",
                schema: "gtfs_management",
                table: "CalendarDates",
                column: "service_id",
                principalSchema: "gtfs_management",
                principalTable: "Services",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_calendars_services_service_id",
                schema: "gtfs_management",
                table: "Calendars",
                column: "service_id",
                principalSchema: "gtfs_management",
                principalTable: "Services",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_fare_attributes_agencies_agency_id",
                schema: "gtfs_management",
                table: "FareAttributes",
                column: "agency_id",
                principalSchema: "gtfs_management",
                principalTable: "Agencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_fare_rules_fare_attributes_fare_attribute_id",
                schema: "gtfs_management",
                table: "FareRules",
                column: "fare_attribute_id",
                principalSchema: "gtfs_management",
                principalTable: "FareAttributes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_fare_rules_routes_route_id",
                schema: "gtfs_management",
                table: "FareRules",
                column: "route_id",
                principalSchema: "gtfs_management",
                principalTable: "Routes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_frequencies_trips_trip_id",
                schema: "gtfs_management",
                table: "Frequencies",
                column: "trip_id",
                principalSchema: "gtfs_management",
                principalTable: "Trips",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_pathways_stops_from_stop_id",
                schema: "gtfs_management",
                table: "Pathways",
                column: "from_stop_id",
                principalSchema: "gtfs_management",
                principalTable: "Stops",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_pathways_stops_to_stop_id",
                schema: "gtfs_management",
                table: "Pathways",
                column: "to_stop_id",
                principalSchema: "gtfs_management",
                principalTable: "Stops",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_routes_agencies_agency_id",
                schema: "gtfs_management",
                table: "Routes",
                column: "agency_id",
                principalSchema: "gtfs_management",
                principalTable: "Agencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_routes_networks_network_id",
                schema: "gtfs_management",
                table: "Routes",
                column: "network_id",
                principalSchema: "gtfs_management",
                principalTable: "Networks",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_stops_levels_level_id",
                schema: "gtfs_management",
                table: "Stops",
                column: "level_id",
                principalSchema: "gtfs_management",
                principalTable: "Levels",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_stops_stops_parent_station_id",
                schema: "gtfs_management",
                table: "Stops",
                column: "parent_station_id",
                principalSchema: "gtfs_management",
                principalTable: "Stops",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_stop_times_location_groups_location_group_id",
                schema: "gtfs_management",
                table: "StopTimes",
                column: "location_group_id",
                principalSchema: "gtfs_management",
                principalTable: "LocationGroups",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_stop_times_stops_stop_id",
                schema: "gtfs_management",
                table: "StopTimes",
                column: "stop_id",
                principalSchema: "gtfs_management",
                principalTable: "Stops",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_stop_times_trips_trip_id",
                schema: "gtfs_management",
                table: "StopTimes",
                column: "trip_id",
                principalSchema: "gtfs_management",
                principalTable: "Trips",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_transfers_routes_from_route_id",
                schema: "gtfs_management",
                table: "Transfers",
                column: "from_route_id",
                principalSchema: "gtfs_management",
                principalTable: "Routes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_transfers_routes_to_route_id",
                schema: "gtfs_management",
                table: "Transfers",
                column: "to_route_id",
                principalSchema: "gtfs_management",
                principalTable: "Routes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_transfers_stops_from_stop_id",
                schema: "gtfs_management",
                table: "Transfers",
                column: "from_stop_id",
                principalSchema: "gtfs_management",
                principalTable: "Stops",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_transfers_stops_to_stop_id",
                schema: "gtfs_management",
                table: "Transfers",
                column: "to_stop_id",
                principalSchema: "gtfs_management",
                principalTable: "Stops",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_transfers_trips_from_trip_id",
                schema: "gtfs_management",
                table: "Transfers",
                column: "from_trip_id",
                principalSchema: "gtfs_management",
                principalTable: "Trips",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_transfers_trips_to_trip_id",
                schema: "gtfs_management",
                table: "Transfers",
                column: "to_trip_id",
                principalSchema: "gtfs_management",
                principalTable: "Trips",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_trips_routes_route_id",
                schema: "gtfs_management",
                table: "Trips",
                column: "route_id",
                principalSchema: "gtfs_management",
                principalTable: "Routes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_trips_services_service_id",
                schema: "gtfs_management",
                table: "Trips",
                column: "service_id",
                principalSchema: "gtfs_management",
                principalTable: "Services",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
