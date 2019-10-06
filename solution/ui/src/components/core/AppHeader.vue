<template>
  <div :class="$options.name"></div>
</template>

<script>
export default {
  name: "AppHeader"
};
</script>

<style lang="scss">
@import "@/scss/abstracts/_colors.scss";

.AppHeader {
  grid-area: header;
  background-color: $primary-color;
  color: white;
}
</style>
