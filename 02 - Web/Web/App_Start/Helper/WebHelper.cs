using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Biz.Core.Models;
using Biz.OG.Models;
using Biz.OG.Services;
using Biz.Core.Security;

public class WebHelper
{
    public enum EmployeeSubType
    {
        Definite_RBVH = 100,
        In_Definite_RBVH = 101,
        Expat_RBVH = 102,
        Seasonal_RBVH = 103,
        Probation_RBVH = 104,
        Intern_RBVH = 105,
        Definite_RBVN = 106,
        In_Definite_RBVN = 107,
        Expat_RBVN = 108,
        M_Level_RBVN = 109,
        Fixed_Term_RBVN = 110,
        GeneralProbation_RBVN = 111,
        M_LevelProbation_RBVN = 112,
        GeneralInternship_RBVN = 113,
        InternshipOverseas_RBVN = 114,
        Definite_HcP = 116,
        In_Definite_HcP = 117,
        Expat_HcP = 118,
        M_Level_HcP = 119,
        Fixed_Term_HcP = 120,
        GeneralProbation_HcP = 121,
        M_LevelProbation_HcP = 122,
        GeneralInternship_HcP = 123,
        InternshipOverseas_HcP = 124,
        Definite_ENG = 126,
        In_Definite_ENG = 127,
        Expat_ENG = 128,
        M_Level_ENG = 129,
        Fixed_Term_ENG = 130,
        GeneralProbation_ENG = 131,
        M_LevelProbation_ENG = 132,
        GeneralInternship_ENG = 133,
        InternshipOverseas_ENG = 134,
    }
   
    
}
