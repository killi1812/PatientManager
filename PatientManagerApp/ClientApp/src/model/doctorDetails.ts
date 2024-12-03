// doctorDto.ts
export interface DoctorDetails {
  guid: string;
  name: string;
  surname: string;
  email: string;
  phone: string;
  //TODO add patients model
  patients: any[];
}
