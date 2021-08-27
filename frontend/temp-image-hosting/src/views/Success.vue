<template>
  <div>
    <div v-if="imageUrl === null">
      <div class="text-center">
        <h1 class="display-4">Oops, something went wrong</h1>
      </div>
    </div>

    <div v-else>
      <div class="text-center">
        <h1 class="display-4">Your file was successfully uploaded</h1>
      </div>

      <p>
        <a
          class="d-block"
          target="_blank"
          rel="noreferrer noopener"
          :href="imageUrl"
          >{{ imageUrl }}</a
        >
      </p>

      <p>
        <button @click="copyUrl" class="btn btn-primary mt-2">Copy URL</button>
      </p>

      <div style="display: flex; justify-content: center">
        <img :src="imageUrl" style="max-width: 100%" />
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Success",

  beforeMount() {
    const imageUrl = this.$route.query.imageUrl;

    this.imageUrl = imageUrl || null;
  },
  data() {
    return {
      imageUrl: null,
    };
  },
  methods: {
    copyUrl() {
      navigator.clipboard.writeText(this.imageUrl);
    },
  },
};
</script>
