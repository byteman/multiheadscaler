
-- Table: weight
CREATE TABLE IF NOT EXISTS weight ( 
    id     INTEGER  PRIMARY KEY AUTOINCREMENT,
    weight DECIMAL,
    s_date DATETIME,
    diff   DECIMAL,
    heads  TEXT 
);

INSERT INTO [weight] ([id], [weight], [s_date], [diff], [heads]) VALUES (1, 0, '2015-11-06 17:40:07', 1, '1,4,5');
INSERT INTO [weight] ([id], [weight], [s_date], [diff], [heads]) VALUES (2, 2, '2015-11-06 17:40:11', 3, '1,4,5');
INSERT INTO [weight] ([id], [weight], [s_date], [diff], [heads]) VALUES (3, 4, '2015-11-06 17:40:13', 5, '1,4,5');

-- Table: formula
CREATE TABLE IF NOT EXISTS formula ( 
    id                 INTEGER PRIMARY KEY AUTOINCREMENT,
    target_weight      DECIMAL,
    packet_per_minitue DECIMAL,
    up_diff            DECIMAL,
    down_diff          DECIMAL,
    stable_time        INTEGER,
    tare_count         INTEGER,
    force_comb         INTEGER,
    no_comb            INTEGER,
    AFC                INTEGER,
    feed_mode          INTEGER,
    feed_in_turn       INTEGER,
    motor_mode         INTEGER,
    multi_feed         INTEGER,
    formula_name       TEXT,
    formula_id         INTEGER,
    open_delay         INTEGER,
    pic_id             INTEGER,
    xzp_strength0      INTEGER,
    xzp_time0          INTEGER,
    xzp_strength1      INTEGER,
    xzp_time1          INTEGER,
    xzp_strength2      INTEGER,
    xzp_time2          INTEGER,
    xzp_strength3      INTEGER,
    xzp_time3          INTEGER,
    xzp_strength4      INTEGER,
    xzp_time4          INTEGER,
    xzp_strength5      INTEGER,
    xzp_time5          INTEGER,
    xzp_strength6      INTEGER,
    xzp_time6          INTEGER,
    xzp_strength7      INTEGER,
    xzp_time7          INTEGER,
    xzp_strength8      INTEGER,
    xzp_time8          INTEGER,
    xzp_strength9      INTEGER,
    xzp_time9          INTEGER 
);

INSERT INTO [formula] ([id], [target_weight], [packet_per_minitue], [up_diff], [down_diff], [stable_time], [tare_count], [force_comb], [no_comb], [AFC], [feed_mode], [feed_in_turn], [motor_mode], [multi_feed], [formula_name], [formula_id], [open_delay], [pic_id], [xzp_strength0], [xzp_time0], [xzp_strength1], [xzp_time1], [xzp_strength2], [xzp_time2], [xzp_strength3], [xzp_time3], [xzp_strength4], [xzp_time4], [xzp_strength5], [xzp_time5], [xzp_strength6], [xzp_time6], [xzp_strength7], [xzp_time7], [xzp_strength8], [xzp_time8], [xzp_strength9], [xzp_time9]) VALUES (1, 300, 40, 10, 0, 10, 20, 200, 0, 1, 1, 2, 3, 4, 'name1', 1, 100,1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
INSERT INTO [formula] ([id], [target_weight], [packet_per_minitue], [up_diff], [down_diff], [stable_time], [tare_count], [force_comb], [no_comb], [AFC], [feed_mode], [feed_in_turn], [motor_mode], [multi_feed], [formula_name], [formula_id], [open_delay], [pic_id], [xzp_strength0], [xzp_time0], [xzp_strength1], [xzp_time1], [xzp_strength2], [xzp_time2], [xzp_strength3], [xzp_time3], [xzp_strength4], [xzp_time4], [xzp_strength5], [xzp_time5], [xzp_strength6], [xzp_time6], [xzp_strength7], [xzp_time7], [xzp_strength8], [xzp_time8], [xzp_strength9], [xzp_time9]) VALUES (2, 400, 40, 5, 0, 20, 10, 200, 1, 1, 1, 2, 3, 4, 'name2', 2, 100,2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
