<template>
  <div :class="$options.name">
    <img :class="`${$options.name}__image`" :src="imageUrl" />
  </div>
</template>

<script>
export default {
  name: "BaseAvatar",

  props: {
    size: String,
    imageUrl: String
  },

  methods: {}
};
</script>

<style lang="scss">
.BaseAvatar {
  border-radius: 50%;
  width: 50px;
  height: 50px;
  overflow: hidden;
  position: relative;

  &__image {
    max-width: 100%;
    max-height: 100%;
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
  }
}
</style>
