import axios from "@/plugins/axios";
import type {Patient} from '@/model/patient';
import {mapForm} from "@/helpers/formHelpers";

const baseUrl = '/patient';

export const createPatient = async (patientDto: Patient) => {
  const formData = mapForm(patientDto)

  const response = await axios.post(`${baseUrl}/CreatePatient`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
  return response;
};

export const updatePatient = async (guid: string, patientDto: Patient) => {
  const formData = mapForm(patientDto)

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
    params: {q, page, n},
  });
  return response;
};

export const getYourPatients = async (page: number = 1, n: number = 10) => {
  const response = await axios.get(`${baseUrl}/GetYorPatients`, {
    params: {page, n},
  });
  return response;
};

export const downloadCsv = async (guid: string) => {
  try {
    const response = await axios.get(`MedicalHistory/export/${guid}`, {
      responseType: 'blob',
    });

    const url = window.URL.createObjectURL(new Blob([response.data]));
    const link = document.createElement('a');
    link.href = url;
    link.setAttribute('download', getFileNameFromContentDisposition(response.headers["content-disposition"]) ?? "unknown")
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  } catch (error) {
    console.error('Error downloading CSV:', error);
  }
};

function getFileNameFromContentDisposition(header: string) {
  const fileNameMatch = header.match(/filename\*?=['"]?([^;\n]*)['"]?/i);
  if (fileNameMatch != null && fileNameMatch[1]) {
    let fileName = fileNameMatch[1];
    if (fileName.startsWith("UTF-8''")) {
      fileName = decodeURIComponent(fileName.replace("UTF-8''", ''));
    }
    return fileName.replace(/['"]/g, '');
  }
  return null;
}
