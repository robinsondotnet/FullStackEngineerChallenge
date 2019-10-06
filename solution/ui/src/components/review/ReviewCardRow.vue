<template>
  <div :class="`${$options.name}`">
    <review-card :review="review" />
    <textarea
      placeholder="Write your feedback here ..."
      :class="`${$options.name}__textArea`"
      v-model="feedbackText"
    ></textarea>
    <button @click="onSendClick(review.id, feedbackText)" :class="[`${$options.name}__button`]">
      <i class="fa fa-envelope"></i>
    </button>
  </div>
</template>
<script>
import ReviewCard from "./ReviewCard.vue";

export default {
  name: "ReviewCardRow",

  components: { ReviewCard },

  props: {
    review: {
      type: Object,
      required: true
    }
  },

  methods: {
    onSendClick(reviewId, feedbackText) {
      this.$emit("on-send", reviewId, feedbackText);
    }
  },

  data() {
    return {
      feedbackText: ""
    };
  }
};
</script>

<style lang="scss">
@import "@/scss/abstracts/_colors.scss";

.ReviewCardRow {
  display: flex;
  flex-direction: row;

  &__textArea {
    width: 50%;
    font-size: 1.5em;
    resize: none;
  }

  &__button {
    width: 15%;
    font-size: 4em;
    color: $secondary-color;
    background-color: #1683de;
  }
}
</style>
