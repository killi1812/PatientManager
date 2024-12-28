export enum ExaminationType {
  GP,
  KRV,
  X_RAY,
  CT,
  MR,
  ULTRA,
  EKG,
  ECHO,
  EYE,
  DERM,
  DENTA,
  MAMMO,
  NEURO
}

export const ExaminationTypeProps = Object.keys(ExaminationType)
  .filter(key => isNaN(Number(key))) // Filter out numeric keys
  .map(key => ({
    title: key,
    value: ExaminationType[key as keyof typeof ExaminationType]
  }));

export const ExaminationTypeText = (type: number): string => {
  switch (type) {
    case ExaminationType.GP:
      return "General physical examination";
    case ExaminationType.KRV:
      return "Blood test";
    case ExaminationType.X_RAY:
      return "X-ray scan";
    case ExaminationType.CT:
      return "CT scan";
    case ExaminationType.MR:
      return "MRI scan";
    case ExaminationType.ULTRA:
      return "Ultrasound";
    case ExaminationType.EKG:
      return "Electrocardiogram";
    case ExaminationType.ECHO:
      return "Echocardiogram";
    case ExaminationType.EYE:
      return "Eye examination";
    case ExaminationType.DERM:
      return "Dermatological examination";
    case ExaminationType.DENTA:
      return "Dental examination";
    case ExaminationType.MAMMO:
      return "Mammography";
    case ExaminationType.NEURO:
      return "Neurological examination";
    default:
      return "Unknown examination type";
  }
};
