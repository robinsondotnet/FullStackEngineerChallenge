<template>
  <layout-default id="app" >
      <router-view />
  </layout-default>
</template>

<script>
import LayoutDefault from "./components/layout/LayoutDefault.vue";

export default {
  components: {
    LayoutDefault
  }
}
</script>

<style lang="scss">
body {
  margin: 0 auto;
}

body::before {
  content: "";
  position: fixed;
  top: -10px;
  left: 0;
  width: 100%;
  height: 10px;
  box-shadow: 0px 0 10px rgba(0, 0, 0, 0.8);
  z-index: 100;
}
</style>
