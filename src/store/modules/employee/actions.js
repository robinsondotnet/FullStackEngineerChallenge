import axios from "axios";

export default {
  // TODO: Handle exceptions and *isLoading flag (this should be concern of the caller component)
  loadEmployees() {
    return axios
      .get("http://localhost:3000/employee")
      .then(response => response.data)
      .then(employees => {
        this.commit("employee/setEmployees", employees);
      });
  }
};
