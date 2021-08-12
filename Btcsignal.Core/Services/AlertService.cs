using Btcsignal.Core.Inerfaces.Repositories;
using Btcsignal.Core.Inerfaces.Services;
using Btcsignal.Core.Models.Dao;
using System.Collections.Generic;
using System.Threading.Tasks;
using Btcsignal.Core.Models.Responses;


namespace Btcsignal.Core.Services
{
    public class AlertService : IAlertService
    {
        private IAlertRepository _alertRepository;


        public AlertService(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }
        public async Task<IEnumerable<Alert>> GetAlertsAdmin()
        {
            var result = await _alertRepository.GetAlertsAdmin();
            return result;
        }
        public async Task<IEnumerable<Alert>> GetAlertsUser(string userId)
        {

            var result = await _alertRepository.GetAlertsUser(userId);
            return result;
        }
        public async Task<AlertCreateResponse> AddAlert(Alert item, string userId)
        {
            var response = new AlertCreateResponse();
            item.UserId = userId;
            item.AlertId = 0;

            //Parameters validation
            if (item.UserId == null)
            {
                response.Message = "User download error. ";
            }
            if (string.IsNullOrEmpty(item.Exchange))
            {
                response.Message = response.Message + "Empty exchange. ";
            }
            if (item.Threshold == 0 & item.Threshold < 0)
            {
                response.Message = response.Message + "Empty Threshold. ";
            }
            if (string.IsNullOrEmpty(item.Currency))
            {
                response.Message = response.Message + "Empty currency. ";
            }
            if (!string.IsNullOrEmpty(response.Message))
            {
                return response;
            }
            
            //Save alert
            var result = await _alertRepository.AddAlert(item);
            if (result != null)
                response = result;
            else
                response.Message = "Adding failed";

            return response;
        }
        public async Task<AlertCreateResponse> UpdateAlert(int alertId, Alert item, string userId)
        {
            var response = new AlertCreateResponse();

            if (item.UserId != userId)
            {
                response.Message = "Bad request: Invalid userId";
                return response;
            }
            if (item.AlertId != alertId)
            {
                response.Message = "Bad request: Invalid alertId";
                return response;
            }

            //Checking if the user has an alert
            var get = await _alertRepository.GetAlert(alertId);
            if (get.UserId != userId || get.AlertId != alertId)
            {
                response.Message = "Alert not found";
                return response;
            }

            var result = await _alertRepository.UpdateAlert(item);
            return result;
        }
        public async Task<Alert> GetAlert(int alertId)
        {
            var result = await _alertRepository.GetAlert(alertId);
            return result;
        }

        public async Task<bool> OnOffAlert(int alertId, string userId)
        {
            //Checking if the user has an alert
            var get = await _alertRepository.GetAlert(alertId);

            if (get == null || get.UserId != userId || get.AlertId != alertId)
            {               
                return false;
            }
            if(get.Active)
                return await _alertRepository.OnOffAlert(alertId, false);
            var result = await _alertRepository.OnOffAlert(alertId, true);
            return result;
        }
    }

}
