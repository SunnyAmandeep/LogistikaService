//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Logistika.Service.Common.Entities.AOC
{
    #pragma warning disable 1573
    using System;

    public partial class LKWPROD_AOC_REPLICATION_DETAILS
    {
        public string PubServer { get; set; }
        public string Sub_Server { get; set; }
        public string Source_DB { get; set; }
        public string dest_db { get; set; }
        public string art_name { get; set; }
        public string dest_table { get; set; }
        public Nullable<int> replicate_ddl { get; set; }
        public int art_artid { get; set; }
        public string creation_script { get; set; }
        public string del_cmd { get; set; }
        public string art_description { get; set; }
        public int filter { get; set; }
        public string filter_clause { get; set; }
        public string ins_cmd { get; set; }
        public int objid { get; set; }
        public int art_pubid { get; set; }
        public byte pre_creation_cmd { get; set; }
        public byte art_status { get; set; }
        public int sync_objid { get; set; }
        public byte type { get; set; }
        public string upd_cmd { get; set; }
        public byte[] schema_option { get; set; }
        public string dest_owner { get; set; }
        public Nullable<int> ins_scripting_proc { get; set; }
        public Nullable<int> del_scripting_proc { get; set; }
        public Nullable<int> upd_scripting_proc { get; set; }
        public string custom_script { get; set; }
        public bool fire_triggers_on_snapshot { get; set; }
        public string pub_description { get; set; }
        public string pub_name { get; set; }
        public int pub_pubid { get; set; }
        public byte repl_freq { get; set; }
        public byte pub_status { get; set; }
        public byte sync_method { get; set; }
        public byte[] snapshot_jobid { get; set; }
        public bool independent_agent { get; set; }
        public bool immediate_sync { get; set; }
        public bool enabled_for_internet { get; set; }
        public bool allow_push { get; set; }
        public bool allow_pull { get; set; }
        public bool allow_anonymous { get; set; }
        public bool immediate_sync_ready { get; set; }
        public bool allow_sync_tran { get; set; }
        public bool autogen_sync_procs { get; set; }
        public Nullable<int> retention { get; set; }
        public bool allow_queued_tran { get; set; }
        public bool snapshot_in_defaultfolder { get; set; }
        public string alt_snapshot_folder { get; set; }
        public string pre_snapshot_script { get; set; }
        public string post_snapshot_script { get; set; }
        public bool compress_snapshot { get; set; }
        public string ftp_address { get; set; }
        public int ftp_port { get; set; }
        public string ftp_subdirectory { get; set; }
        public string ftp_login { get; set; }
        public string ftp_password { get; set; }
        public bool allow_dts { get; set; }
        public bool allow_subscription_copy { get; set; }
        public Nullable<bool> centralized_conflicts { get; set; }
        public Nullable<int> conflict_retention { get; set; }
        public Nullable<int> conflict_policy { get; set; }
        public Nullable<int> queue_type { get; set; }
        public string ad_guidname { get; set; }
        public int backward_comp_level { get; set; }
        public bool allow_initialize_from_backup { get; set; }
        public byte[] min_autonosync_lsn { get; set; }
        public int options { get; set; }
        public Nullable<int> originator_id { get; set; }
        public int sub_artid { get; set; }
        public short srvid { get; set; }
        public byte sub_status { get; set; }
        public byte sync_type { get; set; }
        public string login_name { get; set; }
        public int subscription_type { get; set; }
        public byte[] distribution_jobid { get; set; }
        public byte[] timestamp { get; set; }
        public byte update_mode { get; set; }
        public bool loopback_detection { get; set; }
        public bool queued_reinit { get; set; }
        public byte nosync_type { get; set; }
        public string srvname { get; set; }
    }
}
