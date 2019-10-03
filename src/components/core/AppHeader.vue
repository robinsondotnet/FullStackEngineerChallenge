<template>
  <div :class="$options.name"></div>
</template>

<script>
export default {
  name: "AppHeader"
};
</script>

<style lang="scss">
.AppHeader {
  grid-area: header;
  background-color: orange;
  color: white;
}
</style>
