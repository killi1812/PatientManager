import type {Illness} from "@/model/illness";

export interface Examination {
  guid:string;
  examinationTime: string;
  type:number;
  illness:Illness[]
}
