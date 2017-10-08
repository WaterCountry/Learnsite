function init(width,height,daefile) {

				container = document.getElementById( 'container' );
				camera = new THREE.PerspectiveCamera( 45, width / height, 1, 2000 );
				camera.position.set( 7, 5, 7 );

				scene = new THREE.Scene();

				// collada
				var loader = new THREE.ColladaLoader();
				loader.options.convertUpAxis = true;
				loader.load( daefile, function ( collada ) {

					var object = collada.scene;

					object.scale.set( 0.0025, 0.0025, 0.0025 );
					object.position.set( - 2, 0.2, 0 );

					scene.add( object );
					//camera.lookAt(object.position);
				} );

				//
				var gridHelper = new THREE.GridHelper( 10, 20 );
				scene.add( gridHelper );

				//
				var ambientLight = new THREE.AmbientLight( 0xcccccc );
				scene.add( ambientLight );

				var directionalLight = new THREE.DirectionalLight( 0xffffff );
				directionalLight.position.set( 0, 1, -1 ).normalize();
				scene.add( directionalLight );

				//
				renderer = new THREE.WebGLRenderer();
				renderer.setPixelRatio( window.devicePixelRatio );
				renderer.setSize( window.innerWidth, window.innerHeight );
				container.appendChild( renderer.domElement );

				//
				controls = new THREE.OrbitControls( camera, renderer.domElement );

				//
				stats = new Stats();
				container.appendChild( stats.dom );
				//
				window.addEventListener( 'resize', onWindowResize, false );
}