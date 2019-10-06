<template>
  <base-card :image-url="logo">
    <template #header>
      <h3>{{ `${review.firstName} ${review.lastName}` }}</h3>
    </template>
    <template #body>
      <p :class="`${$options.name}__bodyText`">Age: {{ review.age }}</p>
    </template>
    <template #footer>
      <star-rating
        :class="`${$options.name}__star`"
        :read-only="true"
        :star-size="20"
        :rating="5"
      ></star-rating>
    </template>
  </base-card>
</template>
<script>
import StarRating from "vue-star-rating/src/star-rating";
import BaseCard from "@/components/base/BaseCard.vue";

export default {
  name: "ReviewCard",
  components: { StarRating, BaseCard },

  props: {
    review: {
      type: Object,
      required: true
    }
  },

  data() {
    return {
      logo: "foo"
    };
  }
};
</script>

<style lang="scss">
.ReviewCard {
  &__star {
    justify-content: center;
  }
}
</style>
