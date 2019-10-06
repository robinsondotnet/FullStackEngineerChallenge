<template>
  <div :class="`${$options.name}`">
    <review-card-row
      v-for="review in reviews"
      :key="review.id"
      :review="review"
      @on-send="onSend"
    ></review-card-row>
  </div>
</template>
<script>
import ReviewCardRow from "./ReviewCardRow";
export default {
  name: "ReviewCardRowList",

  components: { ReviewCardRow },

  props: {
    reviews: {
      type: Array,
      default() {
        return [];
      }
    }
  },

  methods: {
    onSend(reviewId, feedbackText) {
      this.$emit("on-send", reviewId, feedbackText);
    }
  }
};
</script>
