import axios from "axios";

// TODO: Handle exceptions

export default {
  submitFeedback(state, { reviewId, feedbackText }) {
    return axios
      .post("/feedback", { reviewId, feedbackText })
      .then(response => {
        if (response.status === 201) this.dispatch("review/loadReviews");
      });
  }
};
