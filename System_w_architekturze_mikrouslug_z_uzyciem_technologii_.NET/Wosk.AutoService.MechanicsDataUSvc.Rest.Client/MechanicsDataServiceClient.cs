using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Wosk.AutoService.MechanicsDataUSvc.Rest.Model;

namespace Wosk.AutoService.MechanicsDataUSvc.Rest.Client
{
    public class MechanicsDataServiceClient : IMechanicsService
    {
        private readonly ServiceClient serviceClient;

        public MechanicsDataServiceClient(string serviceHost, int servicePort)
        {
            Debug.Assert(condition: !String.IsNullOrEmpty(serviceHost) && servicePort > 0);

            this.serviceClient = new ServiceClient(serviceHost, servicePort);
        }


        public MechanicData GetMechanicData(string pesel)
        {
            string callUri = String.Format("Mechanics/GetMechanicData?pesel={0}", pesel);
            MechanicData mechanicData = this.serviceClient.CallWebService<MechanicData>(HttpMethod.Get, callUri);

            return mechanicData;
        }

        public MechanicData[] GetMechanicsData()
        {
            string callUri = String.Format("Mechanics/GetMechanicsData");
            MechanicData[] mechanicsData = this.serviceClient.CallWebService<MechanicData[]>(HttpMethod.Get, callUri);

            return mechanicsData;
        }

        public MechanicPersonalData GetMechanicPersonalData(string pesel)
        {
            string callUri = String.Format("Mechanics/GetMechanicPersonalData?pesel={0}", pesel);
            MechanicPersonalData mechanicPersonalData = this.serviceClient.CallWebService<MechanicPersonalData>(HttpMethod.Get, callUri);

            return mechanicPersonalData;
        }

        public int[] GetAssignedRepairsIds(string pesel)
        {
            string callUri = String.Format("Mechanics/GetAssignedRepairsIds?pesel={0}", pesel);
            int[] assignedRepairs = this.serviceClient.CallWebService<int[]>(HttpMethod.Get, callUri);

            return assignedRepairs;
        }

        public bool AssignNewRepair(int repairId)
        {
            string callUr = String.Format("Mechanics/AssignNewRepair?repairId={0}", repairId);
            StringContent jsonContent = new(JsonSerializer.Serialize(repairId), Encoding.UTF8, "application/json");
            string httpResponse = this.serviceClient.CallWebPostService(callUr, jsonContent).Result; 
            bool success = this.serviceClient.ConvertJson<bool>(httpResponse);

            return success;
        }



    }
}
