import axios from "axios";

// TODO: Handle exceptions

export default {
  loadReviews() {
    return axios
      .get("http://localhost:3000/review")
      .then(response => response.data)
      .then(reviews => {
        this.commit("review/setReviews", reviews);
      });
  }
};
