import type {Prescription} from "@/model/prescription";

export interface Illness {
  guid: string;
  name: string;
  start: string;
  end: string | undefined;
  prescriptions: Prescription[];
}
