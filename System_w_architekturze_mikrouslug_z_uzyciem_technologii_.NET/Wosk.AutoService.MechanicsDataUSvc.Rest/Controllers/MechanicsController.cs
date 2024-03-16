using Microsoft.AspNetCore.Mvc;
using Wosk.AutoService.MechanicsDataUSvc.Model;
using Wosk.AutoService.MechanicsDataUSvc.Logic;
using Wosk.AutoService.MechanicsDataUSvc.Rest.Model;

namespace MechanicsDatabaseUSvc.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MechanicsController : ControllerBase, IMechanicsService
    {
        private readonly ILogger<MechanicsController> logger;

        private readonly Mechanics mechanics;

        public MechanicsController(ILogger<MechanicsController> logger)
        {
            this.logger = logger;
            this.mechanics = new Mechanics();
        }

        [HttpGet]
        [Route("GetMechanicData")]
        public MechanicData GetMechanicData(string pesel)
        {
            Mechanic? mechanic = mechanics.GetMechanic(pesel);
            return (mechanic == null) ? new MechanicData() : DataConverter.ConvertToMechanicData(mechanic);
        }

        [HttpGet]
        [Route("GetMechanicsData")]
        public MechanicData[] GetMechanicsData()
        {
            List<MechanicData> mechanicsList = new List<MechanicData>();
            foreach (Mechanic mechanic in mechanics.GetMechanics())
            {
                mechanicsList.Add(DataConverter.ConvertToMechanicData(mechanic));
            }
            return mechanicsList.ToArray();
        }

        [HttpGet]
        [Route("GetMechanicPersonalData")]
        public MechanicPersonalData GetMechanicPersonalData(string pesel)
        {
            Mechanic? mechanic = mechanics.GetMechanic(pesel);
            return (mechanic == null) ? new MechanicPersonalData() : DataConverter.ConvertToMechanicPersonalData(mechanic);
        }

        [HttpGet]
        [Route("GetAssignedRepairsIds")]
        public int[] GetAssignedRepairsIds(string pesel)
        {
            Mechanic? mechanic = mechanics.GetMechanic(pesel);
            return (mechanic == null) ? new int[0] : mechanic.RepairsIds;
        }


        [HttpPost]
        [Route("AssignNewRepair")]
        public bool AssignNewRepair(int repairId)
        {
            return mechanics.AssignNewRepair(repairId);
        }



    }
}