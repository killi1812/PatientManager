namespace PatientManagerServices.Models.Enums;

/// <summary>
/// Represents different types of medical examinations.
/// </summary>
public enum ExaminationType
{
    /// <summary>
    /// General physical examination.
    /// </summary>
    GP,

    /// <summary>
    /// Blood test.
    /// </summary>
    KRV,

    /// <summary>
    /// X-ray scan.
    /// </summary>
    X_RAY,

    /// <summary>
    /// CT scan.
    /// </summary>
    CT,

    /// <summary>
    /// MRI scan.
    /// </summary>
    MR,

    /// <summary>
    /// Ultrasound.
    /// </summary>
    ULTRA,

    /// <summary>
    /// Electrocardiogram.
    /// </summary>
    EKG,

    /// <summary>
    /// Echocardiogram.
    /// </summary>
    ECHO,

    /// <summary>
    /// Eye examination.
    /// </summary>
    EYE,

    /// <summary>
    /// Dermatological examination.
    /// </summary>
    DERM,

    /// <summary>
    /// Dental examination.
    /// </summary>
    DENTA,

    /// <summary>
    /// Mammography.
    /// </summary>
    MAMMO,

    /// <summary>
    /// Neurological examination.
    /// </summary>
    NEURO
}