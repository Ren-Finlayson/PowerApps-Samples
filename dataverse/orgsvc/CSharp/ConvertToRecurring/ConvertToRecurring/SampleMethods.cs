﻿using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerApps.Samples
{
   public partial class SampleProgram
    {
        // Define the IDs as well as strings needed for this sample.
        public static Guid _appointmentId;
        public static Guid __recurringAppointmentMasterId;

        private static bool prompt = true;
        /// <summary>
        /// Function to set up the sample.
        /// </summary>
        /// <param name="service">Specifies the service to connect to.</param>
        /// 
        private static void SetUpSample(CrmServiceClient service)
        {
            // Check that the current version is greater than the minimum version
            if (!SampleHelpers.CheckVersion(service, new Version("7.1.0.0")))
            {
                //The environment version is lower than version 7.1.0.0
                return;
            }

            CreateRequiredRecords(service);
        }

        private static void CleanUpSample(CrmServiceClient service)
        {
            DeleteRequiredRecords(service, prompt);
        }
        /// <summary>
        /// This method creates any entity records that this sample requires.
        /// Create a new recurring appointment.
        /// </summary>
        public static void CreateRequiredRecords(CrmServiceClient service)
        {
            // Create an appointment
            Appointment newAppointment = new Appointment
            {
                Subject = "Sample Appointment",
                Location = "Office",
                ScheduledStart = DateTime.Now.AddHours(1),
                ScheduledEnd = DateTime.Now.AddHours(2),
            };

            _appointmentId = service.Create(newAppointment);
            Console.WriteLine("Created {0}", newAppointment.Subject);

            return;
        }

        /// <summary>
        /// Deletes any entity records that were created for this sample.
        /// <param name="prompt">Indicates whether to prompt the user to delete 
        /// the records created in this sample.</param>
        /// </summary>
        public static void DeleteRequiredRecords(CrmServiceClient service, bool prompt)
        {
            bool deleteRecords = true;

            if (prompt)
            {
                Console.WriteLine("\nDo you want these entity records to be deleted? (y/n)");
                String answer = Console.ReadLine();

                deleteRecords = (answer.StartsWith("y") || answer.StartsWith("Y"));
            }

            if (deleteRecords)
            {
                service.Delete(RecurringAppointmentMaster.EntityLogicalName,
                    __recurringAppointmentMasterId);

                Console.WriteLine("Entity records have been deleted.");
            }
        }

    }
}