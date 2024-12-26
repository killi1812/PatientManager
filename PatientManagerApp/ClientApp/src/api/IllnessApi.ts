import axios from "@/plugins/axios";
import type { Illness } from '@/model/illness';
import { mapForm } from '@/helpers/formHelpers';

const baseUrl = '/Illness';

export const createIllness = async (medicalHistoryGuid: string, illnessDto: Illness) => {
  const formData = new FormData();
  formData.append('illnessDto', JSON.stringify(illnessDto));

  const response = await axios.post(`${baseUrl}/CreateIllness/${medicalHistoryGuid}`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
  return response;
};

export const updateIllness = async (guid: string, illnessDto: Illness) => {
  const formData = mapForm(illnessDto);
  const response = await axios.put(`${baseUrl}/UpdateIllness/${guid}`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
  return response;
};

export const deleteIllness = async (guid: string) => {
  const response = await axios.delete(`${baseUrl}/DeleteIllness/${guid}`);
  return response;
};

export const getIllness = async (guid: string) => {
  const response = await axios.get(`${baseUrl}/GetIllness/${guid}`);
  return response;
};

export const endIllness = async (guid: string) => {
  const response = await axios.put(`${baseUrl}/EndIllness/${guid}`);
  return response;
};

export const getIllnessesForPatient = async (medicalHistoryGuid: string) => {
  const response = await axios.get(`${baseUrl}/GetIllnessesForPatients/${medicalHistoryGuid}`);
  return response;
};
