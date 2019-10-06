<template>
  <div :class="$options.name">
    <div :class="`${$options.name}__imageWrapper`">
      <img
        :class="`${$options.name}__imageWrapper-image`"
        :src="imageUrl"
        @error="loadFallbackImage"
      />
    </div>
    <div><slot name="header" /></div>
    <div><slot name="body" /></div>
    <div><slot name="footer" /></div>
  </div>
</template>

<script>
export default {
  name: "BaseCard",

  props: {
    imageUrl: String
  },

  methods: {
    loadFallbackImage(evt) {
      evt.target.src = "http://localhost:8080/notfound.png";
    }
  }
};
</script>

<style lang="scss">
.BaseCard {
  border: 0.05em solid rgb(78, 76, 76);
  border-radius: 0.5em;

  &__imageWrapper {
    border-bottom: 0.05em solid rgb(78, 76, 76);
    width: 200px;
    height: 200px;

    &-image {
      width: 200px;
      height: 200px;
    }
  }
}
</style>
