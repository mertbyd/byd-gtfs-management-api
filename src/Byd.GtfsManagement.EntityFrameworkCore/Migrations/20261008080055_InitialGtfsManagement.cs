using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Byd.GtfsManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialGtfsManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "gtfs_management");

            migrationBuilder.CreateTable(
                name: "BikesAlloweds",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bikes_alloweds", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CarsAlloweds",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cars_alloweds", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CemvSupports",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cemv_supports", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ContinuousPickupDropOffs",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_continuous_pickup_drop_offs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ExceptionTypes",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exception_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Feeds",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_feeds", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LocationTypes",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_location_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PickupDropOffTypes",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pickup_drop_off_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RouteTypes",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_route_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "StopAccesses",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stop_accesses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TripDirections",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_trip_directions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WheelchairBoardings",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_wheelchair_boardings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Agencies",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cemv_support_id = table.Column<int>(type: "integer", nullable: true),
                    gtfs_agency_id = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false),
                    timezone = table.Column<string>(type: "text", nullable: false),
                    language = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    fare_url = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_agencies", x => x.id);
                    table.ForeignKey(
                        name: "fk_agencies_cemv_supports_cemv_support_id",
                        column: x => x.cemv_support_id,
                        principalSchema: "gtfs_management",
                        principalTable: "CemvSupports",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_agencies_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gtfs_area_id = table.Column<string>(type: "text", nullable: false),
                    area_name = table.Column<string>(type: "text", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_areas", x => x.id);
                    table.ForeignKey(
                        name: "fk_areas_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingRules",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gtfs_booking_rule_id = table.Column<string>(type: "text", nullable: false),
                    prior_notice_service_id = table.Column<string>(type: "text", nullable: true),
                    booking_type = table.Column<int>(type: "integer", nullable: false),
                    prior_notice_duration_min = table.Column<int>(type: "integer", nullable: true),
                    prior_notice_duration_max = table.Column<int>(type: "integer", nullable: true),
                    prior_notice_last_day = table.Column<int>(type: "integer", nullable: true),
                    prior_notice_last_time = table.Column<string>(type: "text", nullable: true),
                    prior_notice_start_day = table.Column<int>(type: "integer", nullable: true),
                    prior_notice_start_time = table.Column<string>(type: "text", nullable: true),
                    message = table.Column<string>(type: "text", nullable: true),
                    pickup_message = table.Column<string>(type: "text", nullable: true),
                    drop_off_message = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    info_url = table.Column<string>(type: "text", nullable: true),
                    booking_url = table.Column<string>(type: "text", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_booking_rules", x => x.id);
                    table.ForeignKey(
                        name: "fk_booking_rules_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FareLegJoinRules",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    from_network_id = table.Column<string>(type: "text", nullable: true),
                    to_network_id = table.Column<string>(type: "text", nullable: true),
                    from_stop_id = table.Column<string>(type: "text", nullable: true),
                    to_stop_id = table.Column<string>(type: "text", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fare_leg_join_rules", x => x.id);
                    table.ForeignKey(
                        name: "fk_fare_leg_join_rules_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FareLegRules",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    leg_group_id = table.Column<string>(type: "text", nullable: true),
                    network_id = table.Column<string>(type: "text", nullable: true),
                    from_area_id = table.Column<string>(type: "text", nullable: true),
                    to_area_id = table.Column<string>(type: "text", nullable: true),
                    from_timeframe_group_id = table.Column<string>(type: "text", nullable: true),
                    to_timeframe_group_id = table.Column<string>(type: "text", nullable: true),
                    gtfs_fare_product_id = table.Column<string>(type: "text", nullable: false),
                    rule_priority = table.Column<int>(type: "integer", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fare_leg_rules", x => x.id);
                    table.ForeignKey(
                        name: "fk_fare_leg_rules_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FareMedia",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gtfs_fare_media_id = table.Column<string>(type: "text", nullable: false),
                    fare_media_name = table.Column<string>(type: "text", nullable: true),
                    fare_media_type = table.Column<int>(type: "integer", nullable: false),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fare_media", x => x.id);
                    table.ForeignKey(
                        name: "fk_fare_media_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FareProducts",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gtfs_fare_product_id = table.Column<string>(type: "text", nullable: false),
                    gtfs_rider_category_id = table.Column<string>(type: "text", nullable: true),
                    gtfs_fare_media_id = table.Column<string>(type: "text", nullable: true),
                    fare_product_name = table.Column<string>(type: "text", nullable: true),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    currency = table.Column<string>(type: "text", nullable: false),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fare_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_fare_products_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FareTransferRules",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    from_leg_group_id = table.Column<string>(type: "text", nullable: true),
                    to_leg_group_id = table.Column<string>(type: "text", nullable: true),
                    gtfs_fare_product_id = table.Column<string>(type: "text", nullable: true),
                    transfer_count = table.Column<int>(type: "integer", nullable: true),
                    duration_limit = table.Column<int>(type: "integer", nullable: true),
                    duration_limit_type = table.Column<int>(type: "integer", nullable: true),
                    fare_transfer_type = table.Column<int>(type: "integer", nullable: false),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fare_transfer_rules", x => x.id);
                    table.ForeignKey(
                        name: "fk_fare_transfer_rules_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeedInfos",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    publisher_name = table.Column<string>(type: "text", nullable: false),
                    publisher_url = table.Column<string>(type: "text", nullable: false),
                    language = table.Column<string>(type: "text", nullable: false),
                    default_language = table.Column<string>(type: "text", nullable: true),
                    start_date = table.Column<string>(type: "text", nullable: true),
                    end_date = table.Column<string>(type: "text", nullable: true),
                    version = table.Column<string>(type: "text", nullable: true),
                    contact_email = table.Column<string>(type: "text", nullable: true),
                    contact_url = table.Column<string>(type: "text", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_feed_infos", x => x.id);
                    table.ForeignKey(
                        name: "fk_feed_infos_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gtfs_level_id = table.Column<string>(type: "text", nullable: false),
                    level_index = table.Column<double>(type: "double precision", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_levels", x => x.id);
                    table.ForeignKey(
                        name: "fk_levels_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationGroups",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gtfs_location_group_id = table.Column<string>(type: "text", nullable: false),
                    location_group_name = table.Column<string>(type: "text", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_location_groups", x => x.id);
                    table.ForeignKey(
                        name: "fk_location_groups_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationGroupStops",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gtfs_location_group_id = table.Column<string>(type: "text", nullable: false),
                    gtfs_stop_id = table.Column<string>(type: "text", nullable: false),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_location_group_stops", x => x.id);
                    table.ForeignKey(
                        name: "fk_location_group_stops_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Networks",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gtfs_network_id = table.Column<string>(type: "text", nullable: false),
                    network_name = table.Column<string>(type: "text", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_networks", x => x.id);
                    table.ForeignKey(
                        name: "fk_networks_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RiderCategories",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gtfs_rider_category_id = table.Column<string>(type: "text", nullable: false),
                    rider_category_name = table.Column<string>(type: "text", nullable: false),
                    is_default_fare_category = table.Column<int>(type: "integer", nullable: true),
                    eligibility_url = table.Column<string>(type: "text", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rider_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_rider_categories_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RouteNetworks",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gtfs_network_id = table.Column<string>(type: "text", nullable: false),
                    gtfs_route_id = table.Column<string>(type: "text", nullable: false),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_route_networks", x => x.id);
                    table.ForeignKey(
                        name: "fk_route_networks_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gtfs_service_id = table.Column<string>(type: "text", nullable: false),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_services", x => x.id);
                    table.ForeignKey(
                        name: "fk_services_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shapes",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gtfs_shape_id = table.Column<string>(type: "text", nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    longitude = table.Column<double>(type: "double precision", nullable: false),
                    sequence = table.Column<int>(type: "integer", nullable: false),
                    dist_traveled = table.Column<double>(type: "double precision", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shapes", x => x.id);
                    table.ForeignKey(
                        name: "fk_shapes_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StopAreas",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gtfs_area_id = table.Column<string>(type: "text", nullable: false),
                    gtfs_stop_id = table.Column<string>(type: "text", nullable: false),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stop_areas", x => x.id);
                    table.ForeignKey(
                        name: "fk_stop_areas_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Timeframes",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    timeframe_group_id = table.Column<string>(type: "text", nullable: false),
                    gtfs_service_id = table.Column<string>(type: "text", nullable: false),
                    start_time = table.Column<string>(type: "text", nullable: true),
                    end_time = table.Column<string>(type: "text", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_timeframes", x => x.id);
                    table.ForeignKey(
                        name: "fk_timeframes_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    record_id = table.Column<string>(type: "text", nullable: true),
                    record_sub_id = table.Column<string>(type: "text", nullable: true),
                    table_name = table.Column<string>(type: "text", nullable: false),
                    field_name = table.Column<string>(type: "text", nullable: false),
                    language = table.Column<string>(type: "text", nullable: false),
                    translation_text = table.Column<string>(type: "text", nullable: false),
                    field_value = table.Column<string>(type: "text", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_translations", x => x.id);
                    table.ForeignKey(
                        name: "fk_translations_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FareAttributes",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gtfs_fare_id = table.Column<string>(type: "text", nullable: false),
                    agency_id = table.Column<Guid>(type: "uuid", nullable: true),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    currency_type = table.Column<string>(type: "text", nullable: false),
                    payment_method = table.Column<int>(type: "integer", nullable: false),
                    transfers = table.Column<int>(type: "integer", nullable: true),
                    transfer_duration = table.Column<int>(type: "integer", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fare_attributes", x => x.id);
                    table.ForeignKey(
                        name: "fk_fare_attributes_agencies_agency_id",
                        column: x => x.agency_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Agencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_fare_attributes_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stops",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    location_type_id = table.Column<int>(type: "integer", nullable: true),
                    wheelchair_boarding_id = table.Column<int>(type: "integer", nullable: true),
                    parent_station_id = table.Column<Guid>(type: "uuid", nullable: true),
                    level_id = table.Column<Guid>(type: "uuid", nullable: true),
                    stop_access_id = table.Column<int>(type: "integer", nullable: true),
                    gtfs_stop_id = table.Column<string>(type: "text", nullable: false),
                    zone_id = table.Column<string>(type: "text", nullable: true),
                    code = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    tts_name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    latitude = table.Column<double>(type: "double precision", nullable: true),
                    longitude = table.Column<double>(type: "double precision", nullable: true),
                    url = table.Column<string>(type: "text", nullable: true),
                    timezone = table.Column<string>(type: "text", nullable: true),
                    platform_code = table.Column<string>(type: "text", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stops", x => x.id);
                    table.ForeignKey(
                        name: "fk_stops_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stops_levels_level_id",
                        column: x => x.level_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Levels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stops_location_types_location_type_id",
                        column: x => x.location_type_id,
                        principalSchema: "gtfs_management",
                        principalTable: "LocationTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stops_stop_accesses_stop_access_id",
                        column: x => x.stop_access_id,
                        principalSchema: "gtfs_management",
                        principalTable: "StopAccesses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stops_stops_parent_station_id",
                        column: x => x.parent_station_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Stops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stops_wheelchair_boardings_wheelchair_boarding_id",
                        column: x => x.wheelchair_boarding_id,
                        principalSchema: "gtfs_management",
                        principalTable: "WheelchairBoardings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    agency_id = table.Column<Guid>(type: "uuid", nullable: true),
                    route_type_id = table.Column<int>(type: "integer", nullable: false),
                    continuous_pickup_id = table.Column<int>(type: "integer", nullable: true),
                    continuous_drop_off_id = table.Column<int>(type: "integer", nullable: true),
                    network_id = table.Column<Guid>(type: "uuid", nullable: true),
                    cemv_support_id = table.Column<int>(type: "integer", nullable: true),
                    gtfs_route_id = table.Column<string>(type: "text", nullable: false),
                    short_name = table.Column<string>(type: "text", nullable: true),
                    long_name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    url = table.Column<string>(type: "text", nullable: true),
                    color = table.Column<string>(type: "text", nullable: true),
                    text_color = table.Column<string>(type: "text", nullable: true),
                    sort_order = table.Column<int>(type: "integer", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_routes", x => x.id);
                    table.ForeignKey(
                        name: "fk_routes_agencies_agency_id",
                        column: x => x.agency_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Agencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_routes_cemv_supports_cemv_support_id",
                        column: x => x.cemv_support_id,
                        principalSchema: "gtfs_management",
                        principalTable: "CemvSupports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_routes_continuous_pickup_drop_offs_continuous_drop_off_id",
                        column: x => x.continuous_drop_off_id,
                        principalSchema: "gtfs_management",
                        principalTable: "ContinuousPickupDropOffs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_routes_continuous_pickup_drop_offs_continuous_pickup_id",
                        column: x => x.continuous_pickup_id,
                        principalSchema: "gtfs_management",
                        principalTable: "ContinuousPickupDropOffs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_routes_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_routes_networks_network_id",
                        column: x => x.network_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Networks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_routes_route_types_route_type_id",
                        column: x => x.route_type_id,
                        principalSchema: "gtfs_management",
                        principalTable: "RouteTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalendarDates",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    service_id = table.Column<Guid>(type: "uuid", nullable: false),
                    exception_type_id = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_calendar_dates", x => x.id);
                    table.ForeignKey(
                        name: "fk_calendar_dates_exception_types_exception_type_id",
                        column: x => x.exception_type_id,
                        principalSchema: "gtfs_management",
                        principalTable: "ExceptionTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_calendar_dates_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_calendar_dates_services_service_id",
                        column: x => x.service_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Calendars",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    service_id = table.Column<Guid>(type: "uuid", nullable: false),
                    monday = table.Column<bool>(type: "boolean", nullable: false),
                    tuesday = table.Column<bool>(type: "boolean", nullable: false),
                    wednesday = table.Column<bool>(type: "boolean", nullable: false),
                    thursday = table.Column<bool>(type: "boolean", nullable: false),
                    friday = table.Column<bool>(type: "boolean", nullable: false),
                    saturday = table.Column<bool>(type: "boolean", nullable: false),
                    sunday = table.Column<bool>(type: "boolean", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_calendars", x => x.id);
                    table.ForeignKey(
                        name: "fk_calendars_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_calendars_services_service_id",
                        column: x => x.service_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pathways",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gtfs_pathway_id = table.Column<string>(type: "text", nullable: false),
                    from_stop_id = table.Column<Guid>(type: "uuid", nullable: false),
                    to_stop_id = table.Column<Guid>(type: "uuid", nullable: false),
                    pathway_mode = table.Column<int>(type: "integer", nullable: false),
                    is_bidirectional = table.Column<int>(type: "integer", nullable: false),
                    length = table.Column<double>(type: "double precision", nullable: true),
                    traversal_time = table.Column<int>(type: "integer", nullable: true),
                    stair_count = table.Column<int>(type: "integer", nullable: true),
                    max_slope = table.Column<double>(type: "double precision", nullable: true),
                    min_width = table.Column<double>(type: "double precision", nullable: true),
                    signposted_as = table.Column<string>(type: "text", nullable: true),
                    reversed_signposted_as = table.Column<string>(type: "text", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pathways", x => x.id);
                    table.ForeignKey(
                        name: "fk_pathways_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pathways_stops_from_stop_id",
                        column: x => x.from_stop_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Stops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pathways_stops_to_stop_id",
                        column: x => x.to_stop_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Stops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FareRules",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    fare_attribute_id = table.Column<Guid>(type: "uuid", nullable: false),
                    route_id = table.Column<Guid>(type: "uuid", nullable: true),
                    origin_id = table.Column<string>(type: "text", nullable: true),
                    destination_id = table.Column<string>(type: "text", nullable: true),
                    contains_id = table.Column<string>(type: "text", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fare_rules", x => x.id);
                    table.ForeignKey(
                        name: "fk_fare_rules_fare_attributes_fare_attribute_id",
                        column: x => x.fare_attribute_id,
                        principalSchema: "gtfs_management",
                        principalTable: "FareAttributes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_fare_rules_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_fare_rules_routes_route_id",
                        column: x => x.route_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Routes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    route_id = table.Column<Guid>(type: "uuid", nullable: false),
                    service_id = table.Column<Guid>(type: "uuid", nullable: false),
                    trip_direction_id = table.Column<int>(type: "integer", nullable: true),
                    wheelchair_accessible_id = table.Column<int>(type: "integer", nullable: true),
                    bikes_allowed_id = table.Column<int>(type: "integer", nullable: true),
                    cars_allowed_id = table.Column<int>(type: "integer", nullable: true),
                    gtfs_trip_id = table.Column<string>(type: "text", nullable: false),
                    gtfs_shape_id = table.Column<string>(type: "text", nullable: true),
                    block_id = table.Column<string>(type: "text", nullable: true),
                    headsign = table.Column<string>(type: "text", nullable: true),
                    short_name = table.Column<string>(type: "text", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_trips", x => x.id);
                    table.ForeignKey(
                        name: "fk_trips_bikes_alloweds_bikes_allowed_id",
                        column: x => x.bikes_allowed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "BikesAlloweds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_trips_cars_alloweds_cars_allowed_id",
                        column: x => x.cars_allowed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "CarsAlloweds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_trips_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_trips_routes_route_id",
                        column: x => x.route_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Routes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_trips_services_service_id",
                        column: x => x.service_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_trips_trip_directions_trip_direction_id",
                        column: x => x.trip_direction_id,
                        principalSchema: "gtfs_management",
                        principalTable: "TripDirections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_trips_wheelchair_boardings_wheelchair_accessible_id",
                        column: x => x.wheelchair_accessible_id,
                        principalSchema: "gtfs_management",
                        principalTable: "WheelchairBoardings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attributions",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    agency_id = table.Column<Guid>(type: "uuid", nullable: true),
                    route_id = table.Column<Guid>(type: "uuid", nullable: true),
                    trip_id = table.Column<Guid>(type: "uuid", nullable: true),
                    gtfs_attribution_id = table.Column<string>(type: "text", nullable: true),
                    organization_name = table.Column<string>(type: "text", nullable: false),
                    is_producer = table.Column<int>(type: "integer", nullable: true),
                    is_operator = table.Column<int>(type: "integer", nullable: true),
                    is_authority = table.Column<int>(type: "integer", nullable: true),
                    attribution_url = table.Column<string>(type: "text", nullable: true),
                    attribution_email = table.Column<string>(type: "text", nullable: true),
                    attribution_phone = table.Column<string>(type: "text", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attributions", x => x.id);
                    table.ForeignKey(
                        name: "fk_attributions_agencies_agency_id",
                        column: x => x.agency_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Agencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_attributions_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_attributions_routes_route_id",
                        column: x => x.route_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Routes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_attributions_trips_trip_id",
                        column: x => x.trip_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Trips",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Frequencies",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    trip_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_time = table.Column<string>(type: "text", nullable: false),
                    end_time = table.Column<string>(type: "text", nullable: false),
                    headway_secs = table.Column<int>(type: "integer", nullable: false),
                    exact_times = table.Column<int>(type: "integer", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_frequencies", x => x.id);
                    table.ForeignKey(
                        name: "fk_frequencies_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_frequencies_trips_trip_id",
                        column: x => x.trip_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Trips",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StopTimes",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    trip_id = table.Column<Guid>(type: "uuid", nullable: false),
                    stop_id = table.Column<Guid>(type: "uuid", nullable: true),
                    location_group_id = table.Column<Guid>(type: "uuid", nullable: true),
                    pickup_type_id = table.Column<int>(type: "integer", nullable: true),
                    drop_off_type_id = table.Column<int>(type: "integer", nullable: true),
                    continuous_pickup_id = table.Column<int>(type: "integer", nullable: true),
                    continuous_drop_off_id = table.Column<int>(type: "integer", nullable: true),
                    pickup_booking_rule_id = table.Column<string>(type: "text", nullable: true),
                    drop_off_booking_rule_id = table.Column<string>(type: "text", nullable: true),
                    stop_sequence = table.Column<int>(type: "integer", nullable: false),
                    arrival_time = table.Column<string>(type: "text", nullable: true),
                    departure_time = table.Column<string>(type: "text", nullable: true),
                    stop_headsign = table.Column<string>(type: "text", nullable: true),
                    start_pickup_drop_off_window = table.Column<string>(type: "text", nullable: true),
                    end_pickup_drop_off_window = table.Column<string>(type: "text", nullable: true),
                    shape_dist_traveled = table.Column<double>(type: "double precision", nullable: true),
                    timepoint = table.Column<int>(type: "integer", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stop_times", x => x.id);
                    table.ForeignKey(
                        name: "fk_stop_times_continuous_pickup_drop_offs_continuous_drop_off_id",
                        column: x => x.continuous_drop_off_id,
                        principalSchema: "gtfs_management",
                        principalTable: "ContinuousPickupDropOffs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stop_times_continuous_pickup_drop_offs_continuous_pickup_id",
                        column: x => x.continuous_pickup_id,
                        principalSchema: "gtfs_management",
                        principalTable: "ContinuousPickupDropOffs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stop_times_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stop_times_location_groups_location_group_id",
                        column: x => x.location_group_id,
                        principalSchema: "gtfs_management",
                        principalTable: "LocationGroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stop_times_pickup_drop_off_types_drop_off_type_id",
                        column: x => x.drop_off_type_id,
                        principalSchema: "gtfs_management",
                        principalTable: "PickupDropOffTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stop_times_pickup_drop_off_types_pickup_type_id",
                        column: x => x.pickup_type_id,
                        principalSchema: "gtfs_management",
                        principalTable: "PickupDropOffTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stop_times_stops_stop_id",
                        column: x => x.stop_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Stops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stop_times_trips_trip_id",
                        column: x => x.trip_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Trips",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                schema: "gtfs_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    feed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    from_stop_id = table.Column<Guid>(type: "uuid", nullable: true),
                    to_stop_id = table.Column<Guid>(type: "uuid", nullable: true),
                    from_route_id = table.Column<Guid>(type: "uuid", nullable: true),
                    to_route_id = table.Column<Guid>(type: "uuid", nullable: true),
                    from_trip_id = table.Column<Guid>(type: "uuid", nullable: true),
                    to_trip_id = table.Column<Guid>(type: "uuid", nullable: true),
                    transfer_type = table.Column<int>(type: "integer", nullable: false),
                    min_transfer_time = table.Column<int>(type: "integer", nullable: true),
                    extra_properties = table.Column<string>(type: "text", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transfers", x => x.id);
                    table.ForeignKey(
                        name: "fk_transfers_feeds_feed_id",
                        column: x => x.feed_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_transfers_routes_from_route_id",
                        column: x => x.from_route_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Routes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_transfers_routes_to_route_id",
                        column: x => x.to_route_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Routes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_transfers_stops_from_stop_id",
                        column: x => x.from_stop_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Stops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_transfers_stops_to_stop_id",
                        column: x => x.to_stop_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Stops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_transfers_trips_from_trip_id",
                        column: x => x.from_trip_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Trips",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_transfers_trips_to_trip_id",
                        column: x => x.to_trip_id,
                        principalSchema: "gtfs_management",
                        principalTable: "Trips",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "ix_agencies_cemv_support_id",
                schema: "gtfs_management",
                table: "Agencies",
                column: "cemv_support_id");

            migrationBuilder.CreateIndex(
                name: "ix_agencies_feed_id",
                schema: "gtfs_management",
                table: "Agencies",
                column: "feed_id");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_FeedId_GtfsAreaId",
                schema: "gtfs_management",
                table: "Areas",
                columns: new[] { "feed_id", "gtfs_area_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Attributions_FeedId_GtfsAttributionId",
                schema: "gtfs_management",
                table: "Attributions",
                columns: new[] { "feed_id", "gtfs_attribution_id" });

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
                name: "IX_BikesAlloweds_Code",
                schema: "gtfs_management",
                table: "BikesAlloweds",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingRules_FeedId_GtfsBookingRuleId",
                schema: "gtfs_management",
                table: "BookingRules",
                columns: new[] { "feed_id", "gtfs_booking_rule_id" });

            migrationBuilder.CreateIndex(
                name: "IX_CalendarDates_ServiceId_Date",
                schema: "gtfs_management",
                table: "CalendarDates",
                columns: new[] { "service_id", "date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_calendar_dates_exception_type_id",
                schema: "gtfs_management",
                table: "CalendarDates",
                column: "exception_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_calendar_dates_feed_id",
                schema: "gtfs_management",
                table: "CalendarDates",
                column: "feed_id");

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_ServiceId",
                schema: "gtfs_management",
                table: "Calendars",
                column: "service_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_calendars_feed_id",
                schema: "gtfs_management",
                table: "Calendars",
                column: "feed_id");

            migrationBuilder.CreateIndex(
                name: "IX_CarsAlloweds_Code",
                schema: "gtfs_management",
                table: "CarsAlloweds",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CemvSupports_Code",
                schema: "gtfs_management",
                table: "CemvSupports",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContinuousPickupDropOffs_Code",
                schema: "gtfs_management",
                table: "ContinuousPickupDropOffs",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExceptionTypes_Code",
                schema: "gtfs_management",
                table: "ExceptionTypes",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FareAttributes_FeedId_GtfsFareId",
                schema: "gtfs_management",
                table: "FareAttributes",
                columns: new[] { "feed_id", "gtfs_fare_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_fare_attributes_agency_id",
                schema: "gtfs_management",
                table: "FareAttributes",
                column: "agency_id");

            migrationBuilder.CreateIndex(
                name: "IX_FareLegJoinRules_FeedId",
                schema: "gtfs_management",
                table: "FareLegJoinRules",
                column: "feed_id");

            migrationBuilder.CreateIndex(
                name: "IX_FareLegRules_FeedId_GtfsFareProductId",
                schema: "gtfs_management",
                table: "FareLegRules",
                columns: new[] { "feed_id", "gtfs_fare_product_id" });

            migrationBuilder.CreateIndex(
                name: "IX_FareMedia_FeedId_GtfsFareMediaId",
                schema: "gtfs_management",
                table: "FareMedia",
                columns: new[] { "feed_id", "gtfs_fare_media_id" });

            migrationBuilder.CreateIndex(
                name: "IX_FareProducts_FeedId_GtfsFareProductId",
                schema: "gtfs_management",
                table: "FareProducts",
                columns: new[] { "feed_id", "gtfs_fare_product_id" });

            migrationBuilder.CreateIndex(
                name: "IX_FareRules_FeedId_FareAttributeId",
                schema: "gtfs_management",
                table: "FareRules",
                columns: new[] { "feed_id", "fare_attribute_id" });

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
                name: "IX_FareTransferRules_FeedId",
                schema: "gtfs_management",
                table: "FareTransferRules",
                column: "feed_id");

            migrationBuilder.CreateIndex(
                name: "IX_FeedInfos_FeedId",
                schema: "gtfs_management",
                table: "FeedInfos",
                column: "feed_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_Tenant_Name",
                schema: "gtfs_management",
                table: "Feeds",
                columns: new[] { "tenant_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Frequencies_TripId_StartTime",
                schema: "gtfs_management",
                table: "Frequencies",
                columns: new[] { "trip_id", "start_time" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_frequencies_feed_id",
                schema: "gtfs_management",
                table: "Frequencies",
                column: "feed_id");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_FeedId_GtfsLevelId",
                schema: "gtfs_management",
                table: "Levels",
                columns: new[] { "feed_id", "gtfs_level_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationGroups_FeedId_GtfsLocationGroupId",
                schema: "gtfs_management",
                table: "LocationGroups",
                columns: new[] { "feed_id", "gtfs_location_group_id" });

            migrationBuilder.CreateIndex(
                name: "IX_LocationGroupStops_FeedId_GtfsLocationGroupId_GtfsStopId",
                schema: "gtfs_management",
                table: "LocationGroupStops",
                columns: new[] { "feed_id", "gtfs_location_group_id", "gtfs_stop_id" });

            migrationBuilder.CreateIndex(
                name: "IX_LocationTypes_Code",
                schema: "gtfs_management",
                table: "LocationTypes",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Networks_FeedId_GtfsNetworkId",
                schema: "gtfs_management",
                table: "Networks",
                columns: new[] { "feed_id", "gtfs_network_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Pathways_FeedId_GtfsPathwayId",
                schema: "gtfs_management",
                table: "Pathways",
                columns: new[] { "feed_id", "gtfs_pathway_id" },
                unique: true);

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
                name: "IX_PickupDropOffTypes_Code",
                schema: "gtfs_management",
                table: "PickupDropOffTypes",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RiderCategories_FeedId_GtfsRiderCategoryId",
                schema: "gtfs_management",
                table: "RiderCategories",
                columns: new[] { "feed_id", "gtfs_rider_category_id" });

            migrationBuilder.CreateIndex(
                name: "IX_RouteNetworks_FeedId_GtfsRouteId",
                schema: "gtfs_management",
                table: "RouteNetworks",
                columns: new[] { "feed_id", "gtfs_route_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Routes_AgencyId",
                schema: "gtfs_management",
                table: "Routes",
                column: "agency_id");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_Tenant_Feed_GtfsRouteId",
                schema: "gtfs_management",
                table: "Routes",
                columns: new[] { "tenant_id", "feed_id", "gtfs_route_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_routes_cemv_support_id",
                schema: "gtfs_management",
                table: "Routes",
                column: "cemv_support_id");

            migrationBuilder.CreateIndex(
                name: "ix_routes_continuous_drop_off_id",
                schema: "gtfs_management",
                table: "Routes",
                column: "continuous_drop_off_id");

            migrationBuilder.CreateIndex(
                name: "ix_routes_continuous_pickup_id",
                schema: "gtfs_management",
                table: "Routes",
                column: "continuous_pickup_id");

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
                name: "ix_routes_route_type_id",
                schema: "gtfs_management",
                table: "Routes",
                column: "route_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_RouteTypes_Code",
                schema: "gtfs_management",
                table: "RouteTypes",
                column: "code",
                unique: true);

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
                name: "IX_Shapes_FeedId_GtfsShapeId_Sequence",
                schema: "gtfs_management",
                table: "Shapes",
                columns: new[] { "feed_id", "gtfs_shape_id", "sequence" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StopAccesses_Code",
                schema: "gtfs_management",
                table: "StopAccesses",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StopAreas_FeedId_GtfsAreaId_GtfsStopId",
                schema: "gtfs_management",
                table: "StopAreas",
                columns: new[] { "feed_id", "gtfs_area_id", "gtfs_stop_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Stops_ParentStationId",
                schema: "gtfs_management",
                table: "Stops",
                column: "parent_station_id");

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
                name: "ix_stops_location_type_id",
                schema: "gtfs_management",
                table: "Stops",
                column: "location_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_stops_stop_access_id",
                schema: "gtfs_management",
                table: "Stops",
                column: "stop_access_id");

            migrationBuilder.CreateIndex(
                name: "ix_stops_wheelchair_boarding_id",
                schema: "gtfs_management",
                table: "Stops",
                column: "wheelchair_boarding_id");

            migrationBuilder.CreateIndex(
                name: "IX_StopTimes_StopId",
                schema: "gtfs_management",
                table: "StopTimes",
                column: "stop_id");

            migrationBuilder.CreateIndex(
                name: "IX_StopTimes_TripId_StopSequence",
                schema: "gtfs_management",
                table: "StopTimes",
                columns: new[] { "trip_id", "stop_sequence" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_stop_times_continuous_drop_off_id",
                schema: "gtfs_management",
                table: "StopTimes",
                column: "continuous_drop_off_id");

            migrationBuilder.CreateIndex(
                name: "ix_stop_times_continuous_pickup_id",
                schema: "gtfs_management",
                table: "StopTimes",
                column: "continuous_pickup_id");

            migrationBuilder.CreateIndex(
                name: "ix_stop_times_drop_off_type_id",
                schema: "gtfs_management",
                table: "StopTimes",
                column: "drop_off_type_id");

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
                name: "ix_stop_times_pickup_type_id",
                schema: "gtfs_management",
                table: "StopTimes",
                column: "pickup_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Timeframes_FeedId_TimeframeGroupId_GtfsServiceId",
                schema: "gtfs_management",
                table: "Timeframes",
                columns: new[] { "feed_id", "timeframe_group_id", "gtfs_service_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_Feed_Endpoints",
                schema: "gtfs_management",
                table: "Transfers",
                columns: new[] { "feed_id", "from_stop_id", "to_stop_id", "from_trip_id", "to_trip_id", "from_route_id", "to_route_id" },
                unique: true)
                .Annotation("Npgsql:NullsDistinct", false);

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
                name: "IX_Translations_FeedId_Table_Field_Lang_Target",
                schema: "gtfs_management",
                table: "Translations",
                columns: new[] { "feed_id", "table_name", "field_name", "language", "record_id", "record_sub_id", "field_value" });

            migrationBuilder.CreateIndex(
                name: "IX_TripDirections_Code",
                schema: "gtfs_management",
                table: "TripDirections",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_RouteId",
                schema: "gtfs_management",
                table: "Trips",
                column: "route_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_ServiceId",
                schema: "gtfs_management",
                table: "Trips",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_Tenant_Feed_GtfsTripId",
                schema: "gtfs_management",
                table: "Trips",
                columns: new[] { "tenant_id", "feed_id", "gtfs_trip_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_trips_bikes_allowed_id",
                schema: "gtfs_management",
                table: "Trips",
                column: "bikes_allowed_id");

            migrationBuilder.CreateIndex(
                name: "ix_trips_cars_allowed_id",
                schema: "gtfs_management",
                table: "Trips",
                column: "cars_allowed_id");

            migrationBuilder.CreateIndex(
                name: "ix_trips_feed_id",
                schema: "gtfs_management",
                table: "Trips",
                column: "feed_id");

            migrationBuilder.CreateIndex(
                name: "ix_trips_trip_direction_id",
                schema: "gtfs_management",
                table: "Trips",
                column: "trip_direction_id");

            migrationBuilder.CreateIndex(
                name: "ix_trips_wheelchair_accessible_id",
                schema: "gtfs_management",
                table: "Trips",
                column: "wheelchair_accessible_id");

            migrationBuilder.CreateIndex(
                name: "IX_WheelchairBoardings_Code",
                schema: "gtfs_management",
                table: "WheelchairBoardings",
                column: "code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Areas",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "Attributions",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "BookingRules",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "CalendarDates",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "Calendars",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "FareLegJoinRules",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "FareLegRules",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "FareMedia",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "FareProducts",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "FareRules",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "FareTransferRules",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "FeedInfos",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "Frequencies",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "LocationGroupStops",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "Pathways",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "RiderCategories",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "RouteNetworks",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "Shapes",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "StopAreas",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "StopTimes",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "Timeframes",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "Transfers",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "Translations",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "ExceptionTypes",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "FareAttributes",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "LocationGroups",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "PickupDropOffTypes",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "Stops",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "Trips",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "Levels",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "LocationTypes",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "StopAccesses",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "BikesAlloweds",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "CarsAlloweds",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "Routes",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "TripDirections",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "WheelchairBoardings",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "Agencies",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "ContinuousPickupDropOffs",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "Networks",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "RouteTypes",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "CemvSupports",
                schema: "gtfs_management");

            migrationBuilder.DropTable(
                name: "Feeds",
                schema: "gtfs_management");
        }
    }
}
