Imports System
Imports System.Text
Public Class HL7_Send

    Public Function MSH(ByVal Sending_Application As String, _
                        ByVal Sending_Facility As String, _
                        ByVal Receiving_Application As String, _
                        ByVal Receiving_Facility As String, _
                        ByVal Date_Time_of_Message As String, _
                        ByVal Security As String, _
                        ByVal Message_Type As String, _
                        ByVal Message_Control_ID As String, _
                        ByVal Processing_ID As String, _
                        ByVal Version_ID As String)
        MSH = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}", _
                "|", _
                "MSH", _
                "^~\&", _
                Sending_Application, _
                Sending_Facility, _
                Receiving_Application, _
                Receiving_Facility, _
                Date_Time_of_Message, _
                Security, Message_Type, _
                Message_Control_ID, _
                Processing_ID, _
                Version_ID)
        Return MSH
    End Function
    Public Function PID(ByVal Patient_ID As String, _
                        ByVal Patient_Identifier_List As String, _
                        ByVal Alternate_Patient_ID As String, _
                        ByVal Patient_Name As String, _
                        ByVal Mother_Maiden_Name As String, _
                        ByVal BirthDate As String, _
                        ByVal SEX As String, _
                        ByVal Patient_Alias As String, _
                        ByVal Race As String, _
                        ByVal Patient_Address As String, _
                        ByVal Phone_Business As String, _
                        ByVal Phone_Home As String, _
                        ByVal Marital_Status As String, _
                        ByVal Patient_Account_Number As String, _
                        ByVal SSN_Number As String, _
                        ByVal Drivers_License_Number As String, _
                        ByVal Veterans_Military_Status As String, _
                        ByVal Patient_Death_Date As String, _
                        ByVal Patient_Death_Indicator As String, _
                        ByVal Species_Code As String) As String
        Dim PID_N(38) As String
        PID_N(0) = "|"
        PID_N(1) = "PID"
        PID_N(2) = ""
        PID_N(3) = Patient_ID
        PID_N(4) = Patient_Identifier_List
        PID_N(5) = Alternate_Patient_ID
        PID_N(6) = Patient_Name
        PID_N(8) = BirthDate
        PID_N(9) = SEX
        PID_N(10) = Patient_Alias
        PID_N(11) = Race
        PID_N(12) = Patient_Address
        PID_N(14) = Phone_Home
        PID_N(15) = Phone_Business
        PID_N(17) = Marital_Status
        PID_N(19) = Patient_Account_Number
        PID_N(20) = SSN_Number
        PID_N(21) = Drivers_License_Number
        PID_N(28) = Veterans_Military_Status
        PID_N(30) = Patient_Death_Date
        PID_N(31) = Patient_Death_Indicator
        PID_N(36) = Species_Code
        Dim format As String
        format = "{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}" & _
                 "{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}{19}{0}{20}{0}" & _
                 "{21}{0}{22}{0}{23}{0}{24}{0}{25}{0}{26}{0}{27}{0}{28}{0}{29}{0}{30}{0}" & _
                 "{31}{0}{32}{0}{33}{0}{34}{0}{35}{0}{36}{0}{37}{0}{38}{0}"
        PID = String.Format(format, PID_N(0), PID_N(1), PID_N(2), PID_N(3), PID_N(4), PID_N(5), PID_N(6), PID_N(7), PID_N(8), PID_N(9), PID_N(10), _
                            PID_N(11), PID_N(12), PID_N(13), PID_N(14), PID_N(15), PID_N(16), PID_N(17), PID_N(18), PID_N(19), PID_N(20), _
                            PID_N(21), PID_N(22), PID_N(23), PID_N(24), PID_N(25), PID_N(26), PID_N(27), PID_N(28), PID_N(29), PID_N(30), _
                            PID_N(31), PID_N(32), PID_N(33), PID_N(34), PID_N(35), PID_N(36), PID_N(37), PID_N(38))
    End Function
    Public Function PV1(ByVal Patient_Class As String, _
                        ByVal Assigned_Patient_Location As String, _
                        ByVal Patient_Type As String, _
                        ByVal Visit_Number As String, _
                        ByVal Financial_Class As String, _
                        ByVal Discharge_Disposition As String, _
                        ByVal Discharge_Location As String, _
                        ByVal Admit_DateTime As String)
        Dim PV1_N(52) As String
        PV1_N(0) = "|"
        PV1_N(1) = "PV1"
        PV1_N(3) = Patient_Class
        PV1_N(4) = Assigned_Patient_Location
        PV1_N(19) = Patient_Type
        PV1_N(20) = Visit_Number
        PV1_N(21) = Financial_Class
        PV1_N(36) = Discharge_Disposition
        PV1_N(37) = Discharge_Location
        PV1_N(45) = Admit_DateTime
        Dim format As String
        format = "{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}" & _
        "{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}{19}{0}{20}{0}" & _
        "{21}{0}{22}{0}{23}{0}{24}{0}{25}{0}{26}{0}{27}{0}{28}{0}{29}{0}{30}{0}" & _
        "{31}{0}{32}{0}{33}{0}{34}{0}{35}{0}{36}{0}{37}{0}{38}{0}{39}{0}{40}{0}" & _
        "{41}{0}{42}{0}{43}{0}{44}{0}{45}{0}{46}{0}{47}{0}{48}{0}{49}{0}{50}{0}" & _
        "{51}{0}{52}{0}"

        PV1 = String.Format(format, PV1_N(0), PV1_N(1), PV1_N(2), PV1_N(3), PV1_N(4), PV1_N(5), PV1_N(6), PV1_N(7), PV1_N(8), PV1_N(9), PV1_N(10), _
        PV1_N(11), PV1_N(12), PV1_N(13), PV1_N(14), PV1_N(15), PV1_N(16), PV1_N(17), PV1_N(18), PV1_N(19), PV1_N(20), _
        PV1_N(21), PV1_N(22), PV1_N(23), PV1_N(24), PV1_N(25), PV1_N(26), PV1_N(27), PV1_N(28), PV1_N(29), PV1_N(30), _
        PV1_N(31), PV1_N(32), PV1_N(33), PV1_N(34), PV1_N(35), PV1_N(36), PV1_N(37), PV1_N(38), PV1_N(39), PV1_N(40), _
        PV1_N(41), PV1_N(42), PV1_N(43), PV1_N(44), PV1_N(45), PV1_N(46), PV1_N(47), PV1_N(48), PV1_N(49), PV1_N(50), _
        PV1_N(51), PV1_N(52))
    End Function
    
    Public Function ORC(ByVal Order_Control As String, _
                        ByVal Placer_Order_Number As String, _
                        ByVal Filler_Order_Number As String, _
                        ByVal Placer_Group_Number As String, _
                        ByVal Order_Status As String, _
                        ByVal Response_flag As String, _
                        ByVal Quantity_Timing As String, _
                        ByVal Parent As String, _
                        ByVal Date_Time_of_Transaction As String, _
                        ByVal Entered_By As String, _
                        ByVal Verified_By As String, _
                        ByVal Ordering_Provider As String, _
                        ByVal EntererS_Location As String, _
                        ByVal Call_Back_Phone_Number As String, _
                        ByVal Order_Effective_Date_Time As String, _
                        ByVal Order_Control_Code_Reason As String, _
                        ByVal Entering_Organization As String, _
                        ByVal Entering_Device As String, _
                        ByVal Action_By As String) As String
        ORC = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}" & _
                            "{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}{19}{0}{20}{0}", _
                            "|", _
                            "ORC", _
                            Order_Control, _
                            Placer_Order_Number, _
                            Filler_Order_Number, _
                            Placer_Group_Number, _
                            Order_Status, _
                            Response_flag, _
                            Quantity_Timing, _
                            Parent, _
                            Date_Time_of_Transaction, _
                            Entered_By, _
                            Verified_By, _
                            Ordering_Provider, _
                            EntererS_Location, _
                            Call_Back_Phone_Number, _
                            Order_Effective_Date_Time, _
                            Order_Control_Code_Reason, _
                            Entering_Organization, _
                            Entering_Device, _
                            Action_By)
    End Function
    Public Function OBR(ByVal Message_Counter As String, _
                        ByVal Placer_Order_Number As String, _
                        ByVal Filler_Order_Number As String, _
                        ByVal Universal_Service_ID As String, _
                        ByVal Priority As String, _
                        ByVal Requested_Date_time As String, _
                        ByVal Observation_Date_Time As String, _
                        ByVal Observation_End_Date_Time As String, _
                        ByVal Collection_Volume As String, _
                        ByVal Collector_Identifier As String, _
                        ByVal Specimen_Action_Code As String, _
                        ByVal Danger_Code As String, _
                        ByVal Relevant_Clinical_Info As String, _
                        ByVal Specimen_Received_Date_Time As String, _
                        ByVal Specimen_Source As String, _
                        ByVal Odering_provider As String, _
                        ByVal Order_Callback_Phone_Number As String, _
                        ByVal Placer_field_1 As String, _
                        ByVal Placer_field_2 As String, _
                        ByVal Filler_Field_1 As String, _
                        ByVal Filler_Field_2 As String, _
                        ByVal Results_Rpt_Status_Chng_Date_Time As String, _
                        ByVal Charge_to_Practice As String, _
                        ByVal Diagnostic_Serv_Sect_ID As String, _
                        ByVal Result_Status As String, _
                        ByVal Parent_Result As String, _
                        ByVal Quantity_Timing As String, _
                        ByVal Result_Copies_To As String)
        OBR = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}" & _
                            "{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}{19}{0}{20}{0}" & _
                            "{21}{0}{22}{0}{23}{0}{24}{0}{25}{0}{26}{0}{27}{0}{28}{0}{29}{0}", _
                            "|", _
                            "OBR", _
                            Message_Counter, _
                            Placer_Order_Number, _
                            Filler_Order_Number, _
                            Universal_Service_ID, _
                            Priority, _
                            Requested_Date_time, _
                            Observation_Date_Time, _
                            Observation_End_Date_Time, _
                            Collection_Volume, _
                            Collector_Identifier, _
                            Specimen_Action_Code, _
                            Danger_Code, _
                            Relevant_Clinical_Info, _
                            Specimen_Received_Date_Time, _
                            Specimen_Source, _
                            Odering_provider, _
                            Order_Callback_Phone_Number, _
                            Placer_field_1, _
                            Placer_field_2, _
                            Filler_Field_1, _
                            Filler_Field_2, _
                            Results_Rpt_Status_Chng_Date_Time, _
                            Charge_to_Practice, _
                            Diagnostic_Serv_Sect_ID, _
                            Result_Status, _
                            Parent_Result, _
                            Quantity_Timing, _
                            Result_Copies_To)

    End Function
    Public Function OBX(ByVal Message_Counter As String, _
                        ByVal Value_Type As String, _
                        ByVal Observation_Identifier As String, _
                        ByVal Observation_Sub_ID As String, _
                        ByVal Observation_Value As String, _
                        ByVal Units As String, _
                        ByVal References_Range As String, _
                        ByVal Abnormal_Flags As String, _
                        ByVal Probability As String, _
                        ByVal Nature_of_Abnormal_Test As String, _
                        ByVal Observ_Result_Status As String, _
                        ByVal Date_Last_Obs_Normal_Values As String, _
                        ByVal User_Defined_Access_Checks As String, _
                        ByVal Date_Time_of_the_Observation As String, _
                        ByVal ProducerS_ID As String, _
                        ByVal Responsible_Observer As String, _
                        ByVal Observation_Method As String)
        OBX = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}", _
                            "|", _
                            "OBX", _
                            Message_Counter, _
                            Value_Type, _
                            Observation_Identifier, _
                            Observation_Sub_ID, _
                            Observation_Value, _
                            Units, _
                            References_Range, _
                            Abnormal_Flags, _
                            Probability, _
                            Nature_of_Abnormal_Test, _
                            Observ_Result_Status, _
                            Date_Last_Obs_Normal_Values, _
                            User_Defined_Access_Checks, _
                            Date_Time_of_the_Observation, _
                            ProducerS_ID, _
                            Responsible_Observer, _
                            Observation_Method)
    End Function
    Public Function NTE(ByVal Set_ID_NTE As String, ByVal Source_of_Comment As String, ByVal Comment As String) As String
        NTE = String.Format("{1}{0}{2}{0}{3}{0}{4}", "|", "NTE", Set_ID_NTE, Source_of_Comment, Comment)
    End Function
    Public Function MSA(ByVal Acknowledgment_Code As String, _
                        ByVal Message_Control_ID As String, _
                              Optional ByVal Text_Message As String = "", _
                              Optional ByVal Expected_Sequence_Number As String = "", _
                              Optional ByVal Delayed_Acknowledgment_type_not_used As String = "", _
                              Optional ByVal Error_Condition As String = "")
        If Text_Message = "" And Expected_Sequence_Number = "" And Delayed_Acknowledgment_type_not_used = "" And Error_Condition = "" Then
            MSA = String.Format("{1}{0}{2}{0}{3}{0}", _
                    "|", _
                    "MSA", _
                    Acknowledgment_Code, _
                    Message_Control_ID, _
                    Text_Message, _
                    Expected_Sequence_Number, _
                    Delayed_Acknowledgment_type_not_used, _
                    Error_Condition)
        Else
            MSA = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}", _
                    "|", _
                    "MSA", _
                    Acknowledgment_Code, _
                    Message_Control_ID, _
                    Text_Message, _
                    Expected_Sequence_Number, _
                    Delayed_Acknowledgment_type_not_used, _
                    Error_Condition)
        End If

    End Function
End Class
