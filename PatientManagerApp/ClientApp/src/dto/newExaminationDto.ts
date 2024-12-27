import type {Illness} from "@/model/illness";

export interface NewExaminationDto {
  medicalHistoryGuid: string;
  illnessGuid: string | undefined;
  examinationTime: string;
  type:number;
}
