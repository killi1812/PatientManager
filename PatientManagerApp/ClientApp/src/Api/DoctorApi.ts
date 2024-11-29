import axios from "axios";

export const Login =async  (username : string, password: string)  =>{
  const response = await axios.post('/api/doctor/login', {
    username: username,
    password: password,
  })
  return response;
}
