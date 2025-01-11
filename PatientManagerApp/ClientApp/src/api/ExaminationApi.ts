import axios from "@/plugins/axios";
import type {Examination} from '@/model/examination';
import type {NewExaminationDto} from "@/dto/newExaminationDto";
import {mapForm} from "@/helpers/formHelpers";

const baseUrl = '/examination';

export const createExamination = async (examinationDto: NewExaminationDto) => {
  const formData = mapForm(examinationDto)

  const response = await axios.post(`${baseUrl}/CreateExamination`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
  return response;
};

export const updateExamination = async (guid: string, examinationDto: Examination) => {
  const formData = mapForm(examinationDto)

  const response = await axios.put(`${baseUrl}/${guid}`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
  return response;
};

export const uploadExaminationPicture = async (guid: string, formData: FormData) => {
  const response = await axios.post(`/examination/upload/${guid}`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
  return response;
};

export const getExaminationFile = async (fileName: string): Promise<string | undefined> => {
  const response = await axios.get(`${baseUrl}/file/${fileName}`, {responseType: 'blob'})
  return new Promise((resolve, reject) => {
    if (response.status === 200) {
      let reader = new FileReader()
      let blob = new Blob([response.data], {type: response.data.type})
      reader.onload = (event) => {
        resolve(event.target?.result?.toString())
      }
      reader.onerror = () => {
        reject(undefined)
      }
      reader.readAsDataURL(blob)
    }
  })
}

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
