import axios from "axios";

// TODO: Handle exceptions

export default {
  loadReviews() {
    return axios
      .get("/review")
      .then(response => response.data)
      .then(reviews => {
        this.commit("review/setReviews", reviews);
      });
  }
};
