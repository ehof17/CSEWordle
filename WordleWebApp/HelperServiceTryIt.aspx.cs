using System;
using System.Collections.Generic;
using System.Linq;

namespace WordleWebApp
{
    public partial class HelperServiceTryIt : System.Web.UI.Page
    {
        protected void btnCall_Click(object sender, EventArgs e)
        {
           // Get input
            string word = txtActual.Text?.Trim();
            List<int> revealed;

            try
            {
                revealed = txtPositions.Text
                          .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                          .Select(s => int.Parse(s))
                          .ToList();
            }
            catch
            {
                txtResult.Text = "Bad positions list – use commas (e.g. 0,1,2).";
                return;
            }

            //
            try
            {
                using (var client = new HelperServiceReference.Service1Client())
                {
                    string serviceMsg = client.GetHint(word, revealed.ToArray());
                    txtResult.Text = serviceMsg;
                }
            }
            catch (Exception ex)
            {
                txtResult.Text = "Service call failed:\r\n" + ex.Message;
            }
        }
    }
}
