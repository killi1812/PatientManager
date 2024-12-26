import type {DoctorDetails} from "@/model/doctorDetails";
import type {NewDoctor} from "@/model/newDoctor";
import axios from "@/plugins/axios";


const baseUrl = "/doctor";

export const createDoctor = async (doctorDto: DoctorDetails, password: string) => {
  const formData = new FormData();
  formData.append("doctorDto", JSON.stringify(doctorDto));
  formData.append("password", password);

  const response = await axios.post(baseUrl, formData, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
  return response;
};

export const updateDoctor = async (guid: string, doctorDto: DoctorDetails) => {
  const formData = new FormData();
  formData.append("doctorDto", JSON.stringify(doctorDto));

  const response = await axios.put(`${baseUrl}/${guid}`, formData, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
  return response;
};

export const deleteDoctor = async (guid: string) => {
  const response = await axios.delete(`${baseUrl}/${guid}`);
  return response;
};

export const getDoctor = async (guid: string) => {
  const response = await axios.get(`${baseUrl}/${guid}`);
  return response;
};

export const login = async (email: string, password: string) => {
  const response = await axios.post(`${baseUrl}/login`, {
    email: email,
    password: password,
  });
  return response;
};

export const register = async (doctor: NewDoctor) => {
  const rez = await axios.post(`${baseUrl}`, doctor,
    {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    })
  return (rez)
}
