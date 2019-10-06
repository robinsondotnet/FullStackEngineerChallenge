<template>
  <div :class="$options.name">
    <div :class="`${$options.name}__title`">
      <h1>{{ title }}</h1>
    </div>
    <review-card-row-list @on-send="onSend" :reviews="reviews" />
  </div>
</template>
<script>
import ReviewCardRowList from "../components/review/ReviewCardRowList";
import { mapState } from "vuex";

export default {
  name: "PageReviews",

  components: { ReviewCardRowList },

  computed: {
    ...mapState({
      reviews: state => state.review.reviews
    })
  },

  data() {
    return {
      title: "Feedback Required Reviews",
      isLoading: true
    };
  },

  mounted() {
    this.$store
      .dispatch("review/loadReviews")
      .then(() => {
        this.isLoading = false;
      })
      .catch(err => {
        console.log(`Error has ocurred ${err}`);
      });
  },

  methods: {
    onSend(reviewId, feedbackText) {
      this.$store.dispatch("feedback/submitFeedback", {
        reviewId,
        feedbackText
      });
    }
  }
};
</script>
