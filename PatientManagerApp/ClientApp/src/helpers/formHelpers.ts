
export const mapForm = (obj: Record<string, any>): FormData => {
  const formData = new FormData()
  for (const key in obj) {
    if (obj.hasOwnProperty(key)) {
      if(obj[key] == undefined) continue
      formData.append(key, obj[key]);
    }
  }
  return formData;
};
