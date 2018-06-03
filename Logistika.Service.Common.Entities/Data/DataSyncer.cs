using System.Collections.Generic;

namespace Logistika.Service.Common.Entities.Data
{

    /*Server: "KFISSQL-DEV",
        Database: "",
        ConnectionString: "",
        Tables: [
            {
                Table: "[DIVISION]",
                Columns: "[Division_PK],[Client_FK],[Name],[StatusDt],[LeftImagePath],[RightImagePath],[AccessURL],[WebSite],[Status_FK]"
            },
            {
                Table: "[CLIENT]",
                Columns: "[Client_PK],[BillingID],[Name],[PMSID],[OMSID],[WMSID],[TMSID],[Status_FK],[Website],[AccessURL],[LeftImagePath],[RightImagePath],[DataEntryImageFileDropPath],[Comments]"
            }
        ]*/
   public class DataSyncer
   {
       public string Server { get; set; }
       public string Database { get; set; }
       public string ConnectionString { get; set; }
       public List<Table> Tables { get; set; }

    }
   public class SourceDestinationDetial {
       public string SourceServer { get; set; }
       public string ConnectionString { get; set; }
       public IList<DataSyncer> DestinationServers { get; set; }
   }
  
}
