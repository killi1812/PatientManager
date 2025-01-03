import axios from "@/plugins/axios";
import type { Prescription } from '@/model/prescription';
import { mapForm } from '@/helpers/formHelpers';

const baseUrl = '/Prescriptions';

export const createPrescription = async (prescriptionDto: Prescription) => {
  const formData = mapForm(prescriptionDto);
  const response = await axios.post(`${baseUrl}`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
  return response;
};

export const updatePrescription = async (guid: string, prescriptionDto: Prescription) => {
  const formData = mapForm(prescriptionDto);
  const response = await axios.put(`${baseUrl}/${guid}`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
  return response;
};

export const deletePrescription = async (guid: string) => {
  const response = await axios.delete(`${baseUrl}/${guid}`);
  return response;
};

export const getPrescription = async (guid: string) => {
  const response = await axios.get(`${baseUrl}/${guid}`);
  return response;
};

export const getPrescriptionsForMedicalHistory = async (medicalHistoryGuid: string) => {
  const response = await axios.get(`${baseUrl}/medicalHistory/${medicalHistoryGuid}`);
  return response;
};

export const getPrescriptionsForIllness = async (illnessGuid: string) => {
  const response = await axios.get(`${baseUrl}/illness/${illnessGuid}`);
  return response;
};
