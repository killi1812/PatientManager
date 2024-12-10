import axios from 'axios';
import type { Patient } from '@/model/patient';

const baseUrl = '/api/patient';

export const createPatient = async (patientDto: Patient) => {
  const formData = new FormData();
  formData.append('patientDto', JSON.stringify(patientDto));

  const response = await axios.post(`${baseUrl}/CreatePatient`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
  return response;
};

export const updatePatient = async (guid: string, patientDto: Patient) => {
  const formData = new FormData();
  formData.append('patientDto', JSON.stringify(patientDto));

  const response = await axios.put(`${baseUrl}/UpdatePatient/${guid}`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
  return response;
};

export const deletePatient = async (guid: string) => {
  const response = await axios.delete(`${baseUrl}/${guid}`);
  return response;
};

export const getPatient = async (guid: string) => {
  const response = await axios.get(`${baseUrl}/${guid}`);
  return response;
};

export const getPatients = async (q: string | null = null, page: number = 1, n: number = 10) => {
  const response = await axios.get(baseUrl, {
    params: { q, page, n },
  });
  return response;
};
