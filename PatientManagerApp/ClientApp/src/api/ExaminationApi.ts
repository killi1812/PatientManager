import axios from 'axios';
import type { Examination } from '@/model/examination';

const baseUrl = '/api/examination';

export const createExamination = async (examinationDto: Examination) => {
  const formData = new FormData();
  formData.append('examinationDto', JSON.stringify(examinationDto));

  const response = await axios.post(`${baseUrl}/CreateExamination`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
  return response;
};

export const updateExamination = async (guid: string, examinationDto: Examination) => {
  const formData = new FormData();
  formData.append('examinationDto', JSON.stringify(examinationDto));

  const response = await axios.put(`${baseUrl}/${guid}`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
  return response;
};

export const deleteExamination = async (guid: string) => {
  const response = await axios.delete(`${baseUrl}/${guid}`);
  return response;
};

export const getExamination = async (guid: string) => {
  const response = await axios.get(`${baseUrl}/${guid}`);
  return response;
};

export const getAllExaminations = async (medicalHistoryGuid: string) => {
  const response = await axios.get(`${baseUrl}/GetAllExaminations/${medicalHistoryGuid}`);
  return response;
};

export const getExaminationForIllness = async (illnessGuid: string) => {
  const response = await axios.get(`${baseUrl}/GetExaminationForIllness/${illnessGuid}`);
  return response;
};
